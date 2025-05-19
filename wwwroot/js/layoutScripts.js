//function getShoppingCart() {
//    // document.cookie contains all the cookies of our website with the following format
//    // cookie1=value1; cookie2=value2; cookie3=value3;

//    const cookieName = "shopping_cart";
//    let cookiesArray = document.cookie.split(';');

//    for (let i = 0; i < cookiesArray.length; i++) {
//        let cookie = cookiesArray[i];
//        if (cookie.includes(cookieName)) {
//            let cookie_value = cookie.substring(cookie.indexOf("=") + 1);
//            try {
//                let cart = JSON.parse(atob(cookie_value));// for decoad the text as a base64
//                return cart;
//            }
//            catch (exception) {
//                break;
//            }
//        }
//    }
//    return {};
//}

//function saveCart(cart) {
//    let cartStr = atob(JSON.stringify(cart));//btoa for encode the text as a base64
//    // save cookie
//    let d = new Date();
//    d.setDate(d.getDate() + 365); // this cookie expires after 365 days (1 year)
//    let expires = d.toUTCString(); // convert the date to string to stor by Cookies 
//    document.cookie = `shopping_cart=${cartStr};expires=${expires};path=/;SameSite=Strict;Secure`;

//        //"shopping_cart=" + cartStr + ";expires=" + expires + ";path=/; SameSite=Strict; Secure";
//}

//function addToCart(button, id) {// button of add and the id of product
//    let cart = getShoppingCart();//read the cart as Json
//    let quantity = cart[id]
//    if (isNaN(quantity)) {
//        // quantity is Not a Number => set quantity to 1
//        cart[id] = 1
//    }
//    else {
//        cart[id] = Number(quantity) + 1;
//    }

//    saveCart(cart);
//    button.innerHTML = "Added <i class='bi bi-check-lg'></i>";

//    let cartSize = 0;
//    for (var cartItem of Object.entries(cart)) {
//        quantity = cartItem[1]
//        if (isNaN(quantity)) continue;

//        cartSize += Number(quantity)
//    }

//    document.getElementById("CartSize").innerHTML = cartSize
//}

//function increase(id) {
//    let cart = getShoppingCart();

//    let quantity = cart[id]
//    if (isNaN(quantity)) {
//        // quantity is Not a Number => set it to 1
//        cart[id] = 1
//    }
//    else {
//        cart[id] = Number(quantity) + 1;
//    }

//    saveCart(cart);
//    location.reload()
//}

//function decrease(id) {
//    let cart = getShoppingCart();

//    let quantity = cart[id]
//    if (isNaN(quantity)) {
//        // quantity is Not a Number => exit
//        return
//    }

//    quantity = Number(quantity)

//    if (quantity > 1) {
//        cart[id] = quantity - 1
//        saveCart(cart)
//        location.reload()
//    }
//}

//function remove(id) {
//    let cart = getShoppingCart();

//    if (cart[id]) {
//        delete cart[id]
//        saveCart(cart)
//        location.reload()
//    }
//}

//function setCookie(cookieName, cookieValue, expiryInDays) {
//     save cookie
//    let d = new Date();
//    d.setDate(d.getDate() + expiryInDays); // this cookie expires after 365 days (1 year)
//    let expires = d.toUTCString(); // convert the date to string to stor by Cookies 

//    document.cookie = `${cookieName}=${cookieValue};expires=${expires};path=/;SameSite=Strict;Secure`;
//    "shopping_cart=" + cartStr + ";expires=" + expires + ";path=/; SameSite=Strict; Secure";
//}

//function getCookie(cookieName) {

//    if (typeof cookieName === 'undefined' || cookieName === null) {
//        return null;
//    }

//    let cookiesArray = document.cookie.split(';');
//    for (let i = 0; i < cookiesArray.length; i++) {
//        let cookie = cookiesArray[i];
//        if (cookie.includes(cookieName)) {
//            return cookie.substring(cookie.indexOf("=") + 1);
//        }
//    }
//    return null;
//}

/*
create a cookie
    setCookie('shopping_cart', '{"cartVersion": "1.1.1","items": 
    [{"Count": 3,"Description": "Fusion Thor Jt w Autol Sub, Post Appr P Col, Open",
    "id": 1,"Price": 867.95,"ProductName": "Chicken - Wings, Tip Off"},{"Count": 1,
    "Description": "Revision of Monitoring Device in Hepatobiliary Duct, Endo","id": 2,
    "Price": 934.46,"ProductName": "Cake - Miini Cheesecake Cherry"},
    {"Count": 2,"Description": "Drainage of Left Wrist Region with Drain Dev, Open Approach","id": 3,"Price": 300.49,"ProductName": "Mustard - Dry, Powder"},{"Count": 1,"Description": "Excision of Right External Iliac Artery, Open Approach"
    ,"id": 4,"Price": 831.78,"ProductName": "Tomatoes - Orange"},{"Count": 2,"Description": "Excision of Upper Back, Open Approach","id": 5,"Price": 653.16,"ProductName": "Wine - White, Colubia Cresh"}]}')

read cookie from storage
parse cookie value (json > javascript object)

*/
//let cookieValue = getCookie('shopping_cart');

//if (typeof cookieValue === 'undefined' || cookieValue === null) {
//    /*
//    setCookie('shopping_cart', '');
//    */
//    setCookie('shopping_cart', '{"cartVersion": "1.1.1","items": [{"Count": 3,"Description": "Fusion Thor Jt w Autol Sub, Post Appr P Col, Open","id": 1,"Price": 867.95,"ProductName": "Chicken - Wings, Tip Off"},{"Count": 1,"Description": "Revision of Monitoring Device in Hepatobiliary Duct, Endo","id": 2,"Price": 934.46,"ProductName": "Cake - Miini Cheesecake Cherry"},{"Count": 2,"Description": "Drainage of Left Wrist Region with Drain Dev, Open Approach","id": 3,"Price": 300.49,"ProductName": "Mustard - Dry, Powder"},{"Count": 1,"Description": "Excision of Right External Iliac Artery, Open Approach","id": 4,"Price": 831.78,"ProductName": "Tomatoes - Orange"},{"Count": 2,"Description": "Excision of Upper Back, Open Approach","id": 5,"Price": 653.16,"ProductName": "Wine - White, Colubia Cresh"}]}')
//}
//else {
//    try {
//        let cartObj = JSON.parse(cookieValue);
//    } catch (e) {

//    }

//}

function getShoppingCart() {
    //read the shopping cart from cookie called shopping cart and read the cookie from accessable cookie
    // document.cookie contains all the cookies of our website with the following format
    // cookie1=value1; cookie2=value2; cookie3=value3;

    const cookieName = "shopping_cart";
    let cookiesArray = document.cookie.split(';');
    for (let i = 0; i < cookiesArray.length; i++) {
        let cookie = cookiesArray[i];
        if (cookie.includes(cookieName)) {
            let cookie_value = cookie.substring(cookie.indexOf("=") + 1);
            try {
                let cart = JSON.parse(atob(cookie_value));// Decoded: Hello World!atob
                return cart;
            }
            catch (exception) {
                break;
            }
        }
    }
    return {};
}
function saveCart(cart)
{ 
    let cartStr = btoa(JSON.stringify(cart));//Encoded: SGVsbG8gV29ybGQh btoa
    //convert the object of Json to String
    // save cookie
    let d = new Date();
    d.setDate(d.getDate() + 365); // this cookie expires after 365 days (1 year)
    let expires = d.toUTCString();//after make a date then we make a  toUTCString to convert it to string
    document.cookie = "shopping_cart=" + cartStr + ";expires=" + expires + ";path=/; SameSite=Strict; Secure";
}

function addToCart(button, id) {//for the add product button 
    let cart = getShoppingCart();//read a shopping file as a Json object using tghe method that already create

    let quantity = cart[id];//if the quantity dont have this id means have a product with this ID 
    if (isNaN(quantity)) {
        // quantity is Not a Number => set quantity to 1
        cart[id] = 1
    }
    else {
        cart[id] = Number(quantity) + 1;
    }

    saveCart(cart);
    button.innerHTML = "Added <i class='bi bi-check-lg'></i>";

    let cartSize = 0;
    for (var cartItem of Object.entries(cart)) {
        quantity = cartItem[1]
        if (isNaN(quantity)) continue;

        cartSize += Number(quantity)
    }

    document.getElementById("CartSize").innerHTML = cartSize;// add the product to navbar
}


function increase(id) {
    let cart = getShoppingCart();

    let quantity = cart[id]
    if (isNaN(quantity)) {
        // quantity is Not a Number => set it to 1
        cart[id] = 1
    }
    else {
        cart[id] = Number(quantity) + 1;
    }

    saveCart(cart);
    location.reload()
}

function decrease(id) {
    let cart = getShoppingCart();

    let quantity = cart[id]
    if (isNaN(quantity)) {
        // quantity is Not a Number => exit
        return
    }

    quantity = Number(quantity)
    if (quantity > 1) {
        cart[id] = quantity - 1
        saveCart(cart)
        location.reload()
    }
} 
function remove(id) {
    let cart = getShoppingCart();
    if (cart[id]) {
        delete cart[id]
        saveCart(cart)
        location.reload()
    }
}

