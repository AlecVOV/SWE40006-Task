#!/bin/bash
set -e

# Docker Hub username
DOCKERHUB_USER="chilonthon0210"

# Image names
API_IMAGE="reconnaise-api"
FRONTEND_IMAGE="reconnaise-frontend"

echo "=== Logging in to Docker Hub ==="
docker login

echo "=== Tagging images ==="
docker tag code-api:latest "$DOCKERHUB_USER/$API_IMAGE:latest"
docker tag code-frontend:latest "$DOCKERHUB_USER/$FRONTEND_IMAGE:latest"

echo "=== Pushing images ==="
docker push "$DOCKERHUB_USER/$API_IMAGE:latest"
docker push "$DOCKERHUB_USER/$FRONTEND_IMAGE:latest"

echo "=== Done! ==="
echo "Images pushed:"
echo "  - $DOCKERHUB_USER/$API_IMAGE:latest"
echo "  - $DOCKERHUB_USER/$FRONTEND_IMAGE:latest"
