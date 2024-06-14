let DifferentThingies = document.querySelectorAll('[data-different-thingy]');

DifferentThingies.forEach((thingy, index) => {
    thingy.innerHTML = `Different Thingy #${index+1}/${DifferentThingies.length}`;
});
