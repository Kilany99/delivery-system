const express = require('express');
const app = express();
const path = require('path');

app.use(express.static(path.join(__dirname, 'public'))); // Serve static files

app.listen(3000, () => {
    console.log('Customer frontend running on http://localhost:3000');
});