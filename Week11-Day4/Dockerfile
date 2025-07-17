FROM    node:9-alpine
ADD     https://raw.githubusercontent.com/Azure-Samples/acr-build-helloworld-node/master/package.json /
ADD     https://raw.githubusercontent.com/Azure-Samples/acr-build-helloworld-node/master/server.js /
RUN     npm install
EXPOSE  80
CMD     ["node", "server.js"]

# {
#   "passwords": [
#     {
#       "name": "password",
#       "value": "JNhRZWQgTHipqz1Nt+WacD3Pw5KEef/mW2jhhwOn0C+ACRDz06bJ"
#     },
#     {
#       "name": "password2",
#       "value": "5rfVCCoHPj9BG8oGJRH5Wu1XRvHKjwnYvgxe1khrkY+ACRBheiOS"
#     }
#   ],
#   "username": "tharunacr"
# }