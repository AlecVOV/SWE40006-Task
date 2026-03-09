# syntax=docker/dockerfile:1

FROM python:3.12-slim

# Create non-root user for security
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

WORKDIR /app

# Install dependencies first (layer cache efficiency)
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy application source
COPY monitor.py .

# Create log directory and set ownership
RUN mkdir -p /app/logs && chown -R appuser:appgroup /app

# Declare volume for log persistence
VOLUME /app/logs

USER appuser

# Environment defaults (can be overridden at runtime)
ENV LOG_DIR=/app/logs \
    INTERVAL_SECONDS=5

CMD ["python", "-u", "monitor.py"]
