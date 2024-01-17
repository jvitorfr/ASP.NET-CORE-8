import express from 'express';
import cors from 'cors';

const app = express();
const PORT = 32770;

// Middleware to allow CORS from https://localhost:5173
const corsOptions = {
    origin: 'https://localhost:5173',
    methods: 'GET,HEAD,PUT,PATCH,POST,DELETE',
    credentials: true,
    optionsSuccessStatus: 204,
};
app.use(cors(corsOptions));

// Define your routes and other middlewares here

app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});