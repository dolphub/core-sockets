const express = require('express');
const router = express.Router();
const { Client } = require('pg');
const path = require('path');
const logger = require('morgan');
const bodyParser = require('body-parser');

const uuidv4 = require('uuid/v4');

const pgOptions = {
    user: process.env.DB_USER || 'userapp',
    host: process.env.DB_HOST || 'localhost',
    database: process.env.DB_NAME || 'chat',
    password: process.env.DB_PASSWORD || 'abc123',
    port: 5432,
};
console.log(pgOptions)
const client = new Client(pgOptions);

const app = express();

app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false}));

router.get('/users', getUsers);
router.post('/users', createUser);
app.use('/', router);

app.use((req, res, next) => {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

client.connect().then(() => {
    app.listen(process.env.PORT || 3002, () => {
        console.log('Listening for requests...');
    });
}).catch((err) => {
    console.log(err);
});

function getUsers(req, res, next) {
    client.query('SELECT id, "CreatedAt", "emailAddress", username FROM public."Users";', (err, data) => {
        if (err) {
            console.error(err);    
            return res.status(500).json(err);
        }        
        res.json(data.rows);
    })
}

function createUser(req, res, next) {
    const { emailAddress, username } = req.body;
    const id = uuidv4().toString();
    const createdAt = new Date();
    const query = 'INSERT INTO public."Users"(id, "CreatedAt", "emailAddress", username) VALUES($1, $2, $3, $4) RETURNING *';
    const values = [
        id, 
        createdAt,
        emailAddress,
        username
    ];
    client.query(query, values, (err, data) => {
        if (err) {
            console.error(err.stack);    
            return res.status(500).json(err);
        }        
        res.status(201).json(data.rows[0]);
    })
}