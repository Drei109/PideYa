

$(document).ready(function () {
    $(document).on("click", "a[id^='itemCategoria']" , (function () {
        var id = $(this).find("span").attr("id");
        $.ajax({
            url: "/Employee/Pedido/CargarPlatos",
            type: "GET",
            dataType: "json",
            data: { categoriaId: id },
            success: function (data) {
                $("#platos").html("");
                var rows = "<div class='row'>";
                var plate = "";
                $.each(data, function (i, item) {
                    plate +=
                        "<div class='col-6'>"
                        + "<div class='m-portlet m-portlet--bordered-semi m-portlet--full-height  m-portlet--rounded-force'>"
						    + "<div class='m-portlet__head m-portlet__head--fit'>"
							    + "<div class='m-portlet__head-caption'>"
								    + "<div class='m-portlet__head-action'>"
								    + "</div>"
							    + "</div>"
						    + "</div>"
						    + "<div class='m-portlet__body plate-body'>"
							    + "<div class='m-widget19'>"
                                + "<div class='pedido-custom m-widget19__pic m-portlet-fit--top m-portlet-fit--sides' style='background-image: url(" + item.foto + ")'>"
									    //+ "<img src='" +  item.foto + "' alt='' height=130px> "
									    + "<h3 class='m-widget19__title m--font-light'>"
										    //+ "Introducing New Feature"
									    + "</h3>"
								+ "<div class='m-widget19__shadow'></div>"
							    + "</div>"
							    + "<div class='m-widget19__content'>"
									+ "<div class='m-widget19__header'>"
										+ "<div class='m-widget19__info'>"
											+ "<span class='m-widget19__username'>"
												+ item.nombre
											+ "</span>"
                                            + "<input id='nombre" + item.id + "' type='hidden' value='" + item.nombre + "'/>"
											+ "<br>"
											+ "<span class='m-widget19__time'>"
												+ item.id
											+ "</span>"
										+ "</div>"
										+ "<div class='m-widget19__stats'>"
											+ "<span class='m-widget19__number m--font-brand'>"
												+ "S/." + item.precio
											+ "</span>"
                                            + "<input id='precio" + item.id +"' type='hidden' value='" + item.precio +"'/>"
											+ "<span class='m-widget19__comment'>"
												+ "Precio"
											+ "</span>"
										+ "</div>"
									+ "</div>"
									//+ "<div class='m-widget19__body'>"
									//	+ "s"
									//+ "</div>"

                        + "</div>"
                        + "<div class='m-portlet__foot' >"
                            + "<div class='row align-items-center'>"
                                + "<div class='col-lg-10 m--valign-middle row'>"
                                    + "<div id='aumentar" + item.id +"' class='col-lg-3 btn btn-outline-info'>" + "▲" + "</div>"
                                    + "<div class='col-lg-5 col-md-9 col-sm-12' >"
                                        + "<input id='cantidad-plato"+ item.id + "' type='text' class='form-control btn-outline-info' value='0' name='demo1' type='number'>"
                                    + "</div>"
                                    + "<div id='disminuir" + item.id +"' class='col-lg-3 btn btn-outline-info'>" + "▼" + "</div>"
                                + "</div>"
                                + "<div class='col-lg-2 m--align-right'>"
                                    + "<button id='agregar"+ item.id +"' type='submit' class='btn btn-warning'>"
                                        + "Agregar"
                                    + "</button>"
                                + "</div>"
                            + "</div>"
                        + "</div >"
                    + "</div>"
					+ "</div>"
                    + "</div>"
					+ "</div>";
                }
                );
                rows += plate + "</div>";

                $("#platos").append(rows); 
            },
        });
        //alert(rows);
    })),
    $("#plates-main").on("click",
        "div[id^='aumentar']",
        (function() {
            var id = $(this).attr("id").replace(/aumentar/, "");
            var value = $("#cantidad-plato" + id).val();
            $("#cantidad-plato" + id).val(++value);
            }));
    $("#plates-main").on("click",
        "div[id^='disminuir']",
        (function () {
            var id = $(this).attr("id").replace(/disminuir/, "");
            var value = $("#cantidad-plato" + id).val();
            if (value !== 0) { --value }
            $("#cantidad-plato" + id).val(value);
        }));
    $("#plates-main").on("click",
        "button[id^='agregar']",
        (function () {
            var id = $(this).attr("id").replace(/agregar/, "");
            var cantidad = $("#cantidad-plato" + id).val();
            var precio = $("#precio" + id).val();
            var nombre = $("#nombre" + id).val();
            if (parseInt(cantidad) > 0) {
                var fila = "<tr>" +
                    "<input id='idPlato" + id + "' value='" + id + "' type='hiden' />" +
                    "<td>" + nombre + "</td>" +
                    "<td>" + precio + "</td>" +
                    "<td class='m--font-danger'>" + precio * cantidad + "</td>" +
                    "<td style='visibility: collapse'; width:0;>" + id + "</td>" +
                    "</tr>" ;
                $("#pedido-cuerpo").append(fila);
            }
        }));
    $("#plates-main").on("click",
        "button[id^='enviarPedido']",
        (function () {
            var mesaId = $("#mesaId").val();
            var platoIds = [];
            var cantidad = [];
            $("#pedido-cuerpo").find("tr").each(function () {
                //platoIds.push($(this).find("input").val());
                
                var $tds = $(this).find("td");
                cantidad.push($tds.eq(2).text());
                platoIds.push($tds.eq(3).text());
            });
            $.ajax({
                url: "/Employee/Pedido/EnviarPedido",
                type: "GET",
                dataType: "json",
                data: { platoIds: platoIds, cantidad: cantidad, mesaId: mesaId },
                traditional: true,
                success: alert("EXITO")
                //success: location.reload()

        });
        }));

});
   