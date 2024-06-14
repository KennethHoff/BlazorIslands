let thingies = document.querySelectorAll('[data-different-thingy]');

thingies.forEach((thingy, index) => {
    thingy.innerHTML = `Different Thingy #${index+1}/${thingies.length}`;
});
