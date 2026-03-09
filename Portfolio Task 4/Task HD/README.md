# System Pulse Monitor

A lightweight Python-based background service that continuously monitors CPU and RAM usage, logs metrics to a persistent CSV file, and runs inside a Docker container.

---

## Project Structure

```
.
├── monitor.py          # Main monitoring service
├── requirements.txt    # Python dependencies
├── Dockerfile          # Container image definition
├── .dockerignore       # Files excluded from the image build
├── docker-compose.yml  # Orchestration with volumes & resource limits
└── logs/               # (created at runtime) persisted metric CSV files
```

---

## How It Works

`monitor.py` runs in a loop, collecting CPU and RAM metrics via `psutil` every N seconds (default 5) and appending them to `logs/metrics.csv`. The service responds to `SIGTERM`/`SIGINT` for graceful shutdown.

---

## Quick Start

### 1. Build and run with Docker Compose (recommended)

```bash
docker compose up --build -d
```

Logs will be written to `./logs/metrics.csv` on your host machine.

### 2. View live container logs

```bash
docker compose logs -f
```

### 3. Inspect recorded metrics

```bash
cat logs/metrics.csv
```

### 4. Stop the service

```bash
docker compose down
```

---

## Configuration

Environment variables (set in `docker-compose.yml` or `-e` flag):

| Variable           | Default     | Description                          |
|--------------------|-------------|--------------------------------------|
| `INTERVAL_SECONDS` | `5`         | Seconds between each metric snapshot |
| `LOG_DIR`          | `/app/logs` | Directory inside the container where `metrics.csv` is written |

---

## Running Without Docker (local dev)

```bash
pip install -r requirements.txt
python monitor.py
```

---

## Data Persistence

The `docker-compose.yml` mounts `./logs` on the host to `/app/logs` inside the container. Metric data survives container restarts and can be examined independently of Docker.

---

## Resource Limits

The service is constrained to **0.5 CPU cores** and **128 MB RAM** via Docker Compose `deploy.resources`, demonstrating responsible container resource management.

---

## CSV Output Format

| Column         | Description                                |
|----------------|--------------------------------------------|
| `timestamp`    | UTC ISO-8601 timestamp of the sample       |
| `cpu_percent`  | CPU usage across all cores (%)             |
| `ram_used_mb`  | RAM currently in use (MB)                  |
| `ram_total_mb` | Total system RAM (MB)                      |
| `ram_percent`  | RAM usage as a percentage of total         |

---

## Dependencies

- Python 3.12
- [psutil](https://pypi.org/project/psutil/) 5.9.8
