// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//------------------BasketNumeric--------------------------------------------------------
function BasketNumericUp()
{
    $.ajax(
        {
            url: "/UserArea/Home/SepettekiUrunSayisi",
            type: "GET",

            success: function (response) {
                $('#number-id').html(response);
            }
        })
    
}

//-------------------Add Product To Static Basket List------------------------------------------------------------------------------

function AddProductToBasket(productId)
{
    $.ajax({
        url: "/UserArea/Home/UrunleriListeyeEkle",
        type: "POST",
        data: { id: productId },
        success: function (response) {
            // Başarı durumunda yapılacak işlemler
            console.log('Ürün sepete eklendi!');
        },
        error: function (xhr, status, error) {
            // Hata durumunda yapılacak işlemler
            console.log('Hata oluştu: ' + error);
        }
    })
}




//function AddProductToBasket(Userid,Productid,Quantity) {
//    let addedData = {
//        userid: Userid,
//        productid: Productid,
//        quantity=Quantity
//    }
//        var baseURL = 'https://localhost:7270';
//        var endpoint = '/api/Product/CreateOrder';
//        var xhr = new XMLHttpRequest();
//         xhr.open('POST', baseURL+endpoint, true);
//        xhr.setRequestHeader('Content-Type', 'application/json');

//        xhr.onreadystatechange = function() {
//          if (xhr.readyState === 4 && xhr.status === 200) {
//            var response = JSON.parse(xhr.responseText);
//            // Yanıtı işleyin
//          }
//        };

        
//    var jsonData = JSON.stringify(addedData);
//    xhr.send(jsonData);
//}