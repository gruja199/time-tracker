window.blazorLocalStorage = {

    get: key => key in localStorage ? JSON.parse(localStorage[key]) : null,

    set: (key, value) => { localStorag[key] = JSON.stringify(value); }

};