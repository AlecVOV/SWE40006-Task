# Build stage
FROM node:25-alpine AS build
WORKDIR /app

COPY package.json .
RUN npm install

ARG VITE_BACKEND_URL=http://localhost:8000
ENV VITE_BACKEND_URL=$VITE_BACKEND_URL

COPY . .
RUN npm run build

# Runner stage
FROM busybox:1.37 AS runner
WORKDIR /app

COPY --from=build /app/dist .

EXPOSE 8080
CMD ["busybox", "httpd", "-f", "-v", "-p", "8080"]
