# Reconnaise.ai

- School: Swinburne University of Technology, HCMC, Vietnam
- Unit: COS30049 Computing Technology Innovation Project
- Team: G2 `Motor_Ola`

Reconnaise.ai is a project by the student team G2 `Motor_Ola` for the Swinburne unit COS30049, in which we present an AI chatbot application used for the detection of software vulnerabilities.

The work is organised as follow:

- `/api/backend`: AI datasets and model for Assignment 2
- `/frontend`: React frontend for Assignment 3
- `/api`: FastAPI for Assignment 3

Documentation for setting up and using the application are included in the [api](./api/README.md) and [frontend](./frontend/README.md) directories. It is recommended to start-up the backend/API first and the frontend later.

## Tech Stack

**Frontend:** React, BeerCSS

**Server:** FastAPI

## Support

For support, email our team leader Mr. Triet-Thong \<<104171146@student.swin.edu.au>\>.

---


Yes, you can deploy this to **AWS ECS (Elastic Container Service)** using **Fargate** (serverless). Here's the high-level approach:

### Architecture

```
Internet → ALB (Application Load Balancer)
              ├── :80  → Frontend (Fargate task)
              └── :8000 → API (Fargate task)
```

### Steps

1. **Push images to ECR** (or use your Docker Hub images)
   ```bash
   # Create ECR repositories
   aws ecr create-repository --repository-name reconnaise-api
   aws ecr create-repository --repository-name reconnaise-frontend

   # Login, tag, and push
   aws ecr get-login-password | docker login --username AWS --password-stdin <account-id>.dkr.ecr.<region>.amazonaws.com
   docker tag code-api:latest <account-id>.dkr.ecr.<region>.amazonaws.com/reconnaise-api:latest
   docker tag code-frontend:latest <account-id>.dkr.ecr.<region>.amazonaws.com/reconnaise-frontend:latest
   docker push <account-id>.dkr.ecr.<region>.amazonaws.com/reconnaise-api:latest
   docker push <account-id>.dkr.ecr.<region>.amazonaws.com/reconnaise-frontend:latest
   ```

2. **Create ECS Cluster** (Fargate)

3. **Create Task Definitions** — one for each service:
   - **API task**: container port 8000, set `FRONTEND_ORIGIN` env var to ALB URL
   - **Frontend task**: container port 8080 (rebuild image with `VITE_BACKEND_URL` pointing to ALB's API URL)

4. **Create ECS Services** — one per task, attach to ALB target groups

5. **Configure ALB** — route `/` to frontend, `/scan/*`, `/models/*`, `/eda/*`, `/team/*` to API

### Key Considerations

- **Frontend `VITE_BACKEND_URL`**: Since Vite bakes this at build time, you'll need to rebuild the frontend image with the ALB's public DNS as the backend URL
- **Memory**: The API loads 6 ML models — allocate at least **1 GB memory** for the API task
- **Health checks**: API has `/docs` endpoint; frontend serves static files on `/`
- **Cost**: Fargate charges per vCPU/memory per second — this is serverless, no EC2 to manage

You can also use **Docker Hub images directly** with ECS instead of ECR — just reference `yourusername/reconnaise-api:latest` in the task definition.

Would you like me to create the ECS task definition JSON files or a CloudFormation/Terraform template for the deployment?
