# get the base node image
FROM node:alpine as builder

# set the working dir for container
WORKDIR /frontend
EXPOSE 3001

# copy the json file first
COPY package.json /frontend

# install npm dependencies
RUN npm install

# copy other project files
COPY . .

# build the folder
CMD [ "npm", "run", "start" ]

