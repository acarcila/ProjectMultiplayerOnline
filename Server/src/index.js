const dgram = require('dgram');
const server = dgram.createSocket('udp4');

var arrClients = [];

server.on('error', (err) => {
    console.log(`server error:\n${err.stack}`);
    server.close();
});

server.on('message', (msg, info) => {

    var data = stringToJSON(msg);

    console.log(`server got: ${msg} from ${info.address}:${info.port}`);
    var client = { address: info.address, port: info.port }
    if (!arrClients.find(e => (e.address === client.address && e.port === client.port))) {
        arrClients.push(client);
    }

});

setInterval(() => {
    var objTest = {
        positionX: 27,
        positionY: 3,
        positionZ: 1998,
        rotationX: 17,
        rotationY: 9,
        rotationZ: 98,
        id: 54321,
    }
    arrClients.forEach(element => {
        server.send(JSON.stringify(objTest), element.port, element.address, function (error) {
            if (error) {
                client.close();
            } else {
                console.log(`data sent to ${element.address}:${element.port}`);
            }

        });
    });
}, 1000 / 5);

server.on('listening', () => {
    const address = server.address();
    console.log(`server listening ${address.address}:${address.port}`);
});

server.bind(3030);
// Prints: server listening 0.0.0.0:41234

//Methods

stringToJSON = (text) => {
    return JSON.parse(text);
}