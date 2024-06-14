const div = document.createElement('div');
div.innerHTML = "Hello from thingy.js";
document.body.appendChild(div);



const things = document.querySelectorAll('[thing]');
things.forEach(thing => {
    thing.innerHTML = "Hello from thingy.js. I'm a thing!";
});