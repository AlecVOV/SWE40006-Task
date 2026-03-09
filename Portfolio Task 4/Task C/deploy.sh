#!/bin/bash

# Stop execution if any command fails
set -e

# Define your variables here
USERNAME="chilonthon0210"
IMAGE_NAME="python-hello-web"
TAG="latest"

echo "Building the Docker image"
docker build -t $USERNAME/$IMAGE_NAME:$TAG .

echo "Logging into Docker Hub"
docker login

echo "Pushing the image to Docker Hub"
docker push $USERNAME/$IMAGE_NAME:$TAG

echo "Done!"