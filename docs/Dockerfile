FROM node:10

RUN npm i -g http-server

COPY package*.json ./

RUN npm i

COPY . .

RUN npm run-script build

EXPOSE 8080
CMD [ "http-server", ".vuepress/dist" ]
