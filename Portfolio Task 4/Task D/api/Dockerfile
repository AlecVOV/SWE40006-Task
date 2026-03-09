# =============================================================================
# Stage 1: Training - Build models with training dependencies
# =============================================================================
FROM ghcr.io/astral-sh/uv:debian-slim AS trainer

RUN apt-get update && apt-get install -y --no-install-recommends libgomp1 && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Copy dependency files
COPY pyproject.toml uv.lock ./

# Install all dependencies including training extras
RUN uv sync --locked --extra training

# Copy backend source code and data
COPY backend/ ./backend/

# Train models and export results (scripts use relative paths from backend/)
RUN cd backend && uv run python train_model.py

# Generate comparison visualizations from notebook automation
RUN cd backend && uv run python export_comparison_visualizations.py

# =============================================================================
# Stage 2: Runtime - Lean production image with only runtime dependencies
# =============================================================================
FROM ghcr.io/astral-sh/uv:debian-slim AS runtime

RUN apt-get update && apt-get install -y --no-install-recommends libgomp1 && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Copy dependency files
COPY pyproject.toml uv.lock ./

# Install only runtime dependencies (no training/demo extras)
RUN uv sync --locked --no-dev

# Copy application code
COPY main.py ./
COPY backend/ml_detector.py backend/vuln_rules.py ./backend/
COPY backend/metadata/ ./backend/metadata/
COPY backend/pics/ ./backend/pics/

# Copy trained models and documentation from training stage
COPY --from=trainer /app/backend/models/ ./backend/models/
COPY --from=trainer /app/backend/docs/ ./backend/docs/

# Copy dataset (required for the API)
COPY backend/data/ ./backend/data/

EXPOSE 8000
CMD ["uv", "run", "fastapi", "run", "main.py", "--host", "0.0.0.0", "--port", "8000"]
