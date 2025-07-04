FROM node:alpine

WORKDIR /app

COPY . .

RUN npm install -g @angular/cli

RUN npm install

RUN npm install bootstrap

EXPOSE 4201

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4201"]
