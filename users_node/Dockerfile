# Set the base image to Ubuntu
FROM    node:alpine
# FROM ckingdev/alpine-arm-node

# Install nodemon
RUN npm install -g nodemon

# Copy npm package
ADD package.json /tmp/package.json

# Install TODO: Remove bower
RUN cd /tmp && npm install
RUN mkdir -p /src \
    && cp -a /tmp/node_modules /src/

RUN mkdir -p /src
ADD . /src/

# Define working directory
WORKDIR /src

EXPOSE 80

# Run app using nodemon
CMD ["node", "index.js"]