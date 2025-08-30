# ⛏️ Build stage
FROM node:20-alpine AS build
WORKDIR /app
COPY package.json vite.config.js index.html ./
COPY src ./src
COPY nginx.conf ./nginx.conf
RUN npm ci --silent || npm install
RUN npm run build


# 🚀 Runtime stage (nginx)
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY --from=build /app/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]