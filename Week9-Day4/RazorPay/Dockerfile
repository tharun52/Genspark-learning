FROM node:alpine AS builder

WORKDIR /app

COPY . .

RUN npm install
RUN npm run build -- --configuration production

FROM nginx:alpine

COPY --from=builder /app/dist/RazorPay/browser /usr/share/nginx/html

EXPOSE 4201

CMD ["nginx", "-g", "daemon off;"]
