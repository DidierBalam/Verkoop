let iIdProducto;
let Datatables;
let Msn = "Por favor seleccione un producto";
let iEstatus = true;
/**
 * Obtiene la vista de en modal
 * @param {any} cTipo
 * @param {any} cUrel
 * @param {any} Data
 * @param {any} Funcion
 */
function ObtenarVista(cTipo, cUrel, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrel,
        async: false,
        data: Data,
        dataType: "HTML",
        success: function (response) {
            $("#Modal").html(response);
            $('#Modal').modal({ show: true });

            if (Funcion) {
                window[Funcion]();
            }
        }
    });
}

/**
 * Guarda la categoría ingresada en el apartado.
 * @param {any} cNombre nombre de la categoría a guardar.
 */
function GuardarCategoria(cNombre) {
    let Data = {};
    Data["cNombre"] = JSON.stringify(cNombre);

    ObtenarMetodoControlador("POST", "../Categoria/GuardarCategoria", Data).then((objRespuesta) => {
        Swal.fire({
            icon: objRespuesta.EstadoConsulta,
            title: objRespuesta.EstadoConsulta,
            text: objRespuesta.Mensaje
        })
        $("#cNombre").val("") // limpia el campo especifico con su id.
        $("#lstCategoria").html("") // limpia el div.
        LlenarTarjeta(); // Llama la funcion para recargar el div.
    })
}

/**
 * Obtiene el id y estatus del producto, para desactivar el producto
 * @param {any} _iIdProducto id del producto.
 * @param {any} iEstatus estatus del producto
 */
function DeshabilitarProducto(_iIdProducto, iEstatus) {
    let Data = {};

    Data["iIdProducto"] = JSON.parse(_iIdProducto);
    Data["iEstatus"] = JSON.stringify(iEstatus);

    Swal.fire({
        title: '¿Está seguro que desea Eliminar el producto?',
        text: "Una vez ejecutada la acción no podrá ser revertida.",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, deseo Eliminar',
        cancelButtonText: 'No, cancelar',
    }).then((result) => {
        if (result.value) {
            ObtenarMetodoControlador("POST", "../Producto/Eliminar", Data).then((objRespuesta) => {
                Swal.fire({
                    icon: objRespuesta.EstadoConsulta,
                    title: objRespuesta.EstadoConsulta,
                    text: objRespuesta.Mensaje
                })
                Datatables.ajax.reload();
                iIdProducto = 0;
            })
        }
    })
}

/**
 * Ejecuta un método a través del nombre.
 * @param {any} cTipo GET/POST
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} Data Información a actualizar o guardar.
 */
function ObtenarMetodoControlador(cTipo, cUrl, Data) {
    return new Promise((objRespuesta) => {
        $.ajax({
            url: cUrl,
            type: cTipo,
            data: Data,
            async: false,
            dataType: 'JSON',
            success: function (Respuesta) {
                objRespuesta(Respuesta);
            }
        });
    })
}