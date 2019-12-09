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

function DeshabilitarProducto(iIdProducto, iEstatus) {
    let Data = {};

    Data["iIdProducto"] = JSON.parse(iIdProducto);
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
                //alert(objRespuesta.Mensaje);
                Swal.fire({
                    icon: objRespuesta.EstadoConsulta,
                    title: objRespuesta.EstadoConsulta,
                    text: objRespuesta.Mensaje
                })
                
                
            })
        }
    })
}

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
                Datatables.ajax.reload();
                iIdProducto = 0;
            }
        });
    })

}