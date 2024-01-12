# !/bin/bash
# Script that applies the k8s configurations

# Redis cache
kubectl apply -f ./k8s/redis/redis-config.yaml
kubectl apply -f ./k8s/redis/redis-depl.yaml
kubectl apply -f ./k8s/redis/redis-srv.yaml

# Database concerns
kubectl apply -f ./k8s/database/database-config.yaml
kubectl apply -f ./k8s/database/database-secret.yaml
kubectl apply -f ./k8s/database/database-depl.yaml

# catalog-service concerns
kubectl apply -f ./k8s/catalog-service/catalog-config.yaml
kubectl apply -f ./k8s/catalog-service/catalog-depl.yaml

# identity-service concerns
kubectl apply -f ./k8s/identity-service/identity-config.yaml 
kubectl apply -f ./k8s/identity-service/identity-depl.yaml

# Yarp load-balancer concerns
kubectl apply -f ./k8s/load-balancer/load-balancer-config.yaml
kubectl apply -f ./k8s/load-balancer/load-balancer-depl.yaml

# Web frontend deployments
kubectl apply -f ./k8s/web/web-config.yaml
kubectl apply -f ./k8s/web/web-depl.yaml
