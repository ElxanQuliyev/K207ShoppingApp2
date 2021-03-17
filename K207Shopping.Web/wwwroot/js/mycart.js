let cookieVal = Cookies.get("k207Cart");
if (typeof cookieVal !== "undefined" && cookieVal != '') {
    cookieVal = [...cookieVal.split('-')]
    $(".header-action-num").text(cookieVal.length)
}
let productIds = cookieVal ?? [];
$(".add-to-cart").click(function () {
    const productId = $(this).attr("murad");
    const plusminus = $(".cart-plus-minus-box");
    console.log(plusminus)
    if (plusminus.length>0) {
        let plusVal = plusminus.val();
        console.log(plusVal)
        for (var i = 0;i<plusVal; i++) {
            productIds.push(productId)
        }
        $(".cart-plus-minus-box").val(1)
    } else {
        productIds.push(productId)
    }
    Cookies.set("k207Cart", productIds.join('-'), { expires: 7 })
    $(".header-action-num").text(productIds.length)

    Swal.fire({
        position: 'bottom-end',
        icon: 'success',
        title: 'Səbətə əlavə olundu',
        showConfirmButton: false,
        timer: 1500
    })
})