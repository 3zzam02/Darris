

var selection = document.querySelector('select');
var result = document.getElementById('delivery');
var Total = document.getElementById('Total');



 selection.addEventListener('change', () => {
    result.innerText = selection.options[selection.selectedIndex].value
 })

 Total=selection.addEventListener('change', () => {
   Total.innerText= Total.value+selection.options[selection.selectedIndex].value
 }) 
document.getElementById('Total').innerHTML = Total;