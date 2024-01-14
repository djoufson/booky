#!/bin/bash
# Setup (build-publish) the necessary Docker images to deploy our infrastructure on k8s

# Load DockerHub username from .env file
if [ -f .env ]; then
    export $(grep -v '^#' .env | xargs)
fi

# Build Docker images
docker-compose -f docker-compose.build.yml config --services | xargs -I {} docker-compose -f docker-compose.build.yml build {}

# Set DockerHub username from .env file
DOCKER_USERNAME=${DOCKER_USERNAME:-djoufson}

IMAGE_NAME_PREFIX=$DOCKER_USERNAME/booky # Replace with your DockerHub repository name

# Login to DockerHub
docker login -u $DOCKERHUB_USERNAME -p $DOCKERHUB_PASSWORD

# Loop through the services in docker-compose.build.yml
for service in $(docker-compose -f docker-compose.build.yml config --services); do
    # Tag Docker images
    docker tag $service:latest $IMAGE_NAME_PREFIX-$service:latest
    echo "Docker image tagged as $IMAGE_NAME_PREFIX-$service:latest"

    # Push Docker images to DockerHub
    docker push $IMAGE_NAME_PREFIX/$service:latest
    echo "Docker image pushed to DockerHub: $IMAGE_NAME_PREFIX/$service:latest"
done
