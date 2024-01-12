#!/bin/bash
# Script that undoes the k8s configurations and deletes resources

# Web frontend deployments
kubectl delete -f ./k8s/web/web-depl.yaml
kubectl delete -f ./k8s/web/web-config.yaml

# Yarp load-balancer concerns
kubectl delete -f ./k8s/load-balancer/load-balancer-depl.yaml
kubectl delete -f ./k8s/load-balancer/load-balancer-config.yaml

# Identity service concerns
kubectl delete -f ./k8s/identity-service/identity-depl.yaml
kubectl delete -f ./k8s/identity-service/identity-config.yaml

# Catalog service concerns
kubectl delete -f ./k8s/catalog-service/catalog-depl.yaml
kubectl delete -f ./k8s/catalog-service/catalog-config.yaml

# Database concerns
kubectl delete -f ./k8s/database/database-depl.yaml
kubectl delete -f ./k8s/database/database-secret.yaml
kubectl delete -f ./k8s/database/database-config.yaml

# Redis cache
kubectl delete -f ./k8s/redis/redis-srv.yaml
kubectl delete -f ./k8s/redis/redis-depl.yaml
kubectl delete -f ./k8s/redis/redis-config.yaml
