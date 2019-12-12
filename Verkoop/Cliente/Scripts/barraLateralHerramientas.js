
//BOTÓN PARA IR AL CARRITO DE COMPRAS
$(document).on('click', '#btnProducto', function () {

    window.location.href = "/Cliente/Producto/Catalogo";

});

//BOTÓN PARA IR AL CARRITO DE COMPRAS
$(document).on('click', '#tbnCarrito', function () {

    window.location.href = "/Cliente/carritoCompras/CarritoCompras";

});

//BOTÓN PARA CERRAR SESIÓN
$(document).on('click', '#btnSalir',function () {

    CerrarSesion();

});

