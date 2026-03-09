"""
System Pulse Monitor
Continuously monitors CPU and RAM usage, logging metrics to a CSV file.
"""

import csv
import logging
import os
import signal
import sys
import time
from datetime import datetime

import psutil

# --- Configuration ---
LOG_DIR = os.environ.get("LOG_DIR", "/app/logs")
LOG_FILE = os.path.join(LOG_DIR, "metrics.csv")
INTERVAL = int(os.environ.get("INTERVAL_SECONDS", "5"))

# --- Logging ---
logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(message)s",
    handlers=[logging.StreamHandler(sys.stdout)],
)
logger = logging.getLogger("system-pulse-monitor")

# --- Graceful shutdown ---
_running = True


def _handle_signal(signum, frame):
    global _running
    logger.info("Shutdown signal received (%s). Stopping...", signal.Signals(signum).name)
    _running = False


signal.signal(signal.SIGTERM, _handle_signal)
signal.signal(signal.SIGINT, _handle_signal)


def collect_metrics() -> dict:
    """Collect current CPU and RAM metrics."""
    cpu_percent = psutil.cpu_percent(interval=1)
    vm = psutil.virtual_memory()
    return {
        "timestamp": datetime.utcnow().isoformat(timespec="seconds") + "Z",
        "cpu_percent": cpu_percent,
        "ram_used_mb": round(vm.used / 1024 / 1024, 2),
        "ram_total_mb": round(vm.total / 1024 / 1024, 2),
        "ram_percent": vm.percent,
    }


def ensure_log_dir():
    os.makedirs(LOG_DIR, exist_ok=True)


def write_header_if_needed(filepath: str):
    """Write CSV header if file does not yet exist."""
    if not os.path.exists(filepath):
        with open(filepath, "w", newline="") as f:
            writer = csv.DictWriter(
                f,
                fieldnames=["timestamp", "cpu_percent", "ram_used_mb", "ram_total_mb", "ram_percent"],
            )
            writer.writeheader()


def append_metrics(filepath: str, metrics: dict):
    with open(filepath, "a", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=list(metrics.keys()))
        writer.writerow(metrics)


def main():
    ensure_log_dir()
    write_header_if_needed(LOG_FILE)
    logger.info("System Pulse Monitor started. Logging to: %s (interval: %ds)", LOG_FILE, INTERVAL)

    while _running:
        try:
            metrics = collect_metrics()
            append_metrics(LOG_FILE, metrics)
            logger.info(
                "CPU: %s%% | RAM: %s%% (%s / %s MB)",
                metrics["cpu_percent"],
                metrics["ram_percent"],
                metrics["ram_used_mb"],
                metrics["ram_total_mb"],
            )
        except Exception as exc:
            logger.error("Failed to collect/write metrics: %s", exc)

        # Sleep in small increments so we respond to shutdown signals quickly
        for _ in range(INTERVAL):
            if not _running:
                break
            time.sleep(1)

    logger.info("System Pulse Monitor stopped.")


if __name__ == "__main__":
    main()
