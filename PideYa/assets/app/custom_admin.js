$(document).ready(function () {
    $("#empresa").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/Restaurantes/Search",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#empresa_id_fk").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete('search', ' ');
        });

    $("#empresa_nombre").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/Restaurantes/Search",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#empresa_id_fk").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete('search', ' ');
        });

    // Admin/Restaurantes/Create
    $("#usuarioASP").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/EmpresasRestaurantesUsuarios/SearchUser",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#usuarioASP_fk_Id").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete('search', ' ');
        });

    // Admin/Restaurantes/Create
    $("#restaurante2").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/EmpresasRestaurantesUsuarios/SearchRestaurante",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, restId: $("#empresa_id_fk").val(), maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#restaurante_id_fk").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete('search', ' ');
        });

    // User/Procesos/Pedido
    $("#mesa_pedido_cabecera_search").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Manager/Mesas/GetRestaurantTables/",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, id: $("#restaurante_id").val(), maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#PedidoCabecera_mesa_id_fk").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete("search", " ");
        });

    $("#pedido_detalle_platos_search").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Manager/Platos/GetRestaurantDishes/",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term, id: $("#Restaurante_restaurante_id").val(), maxResults: 10 },
                    minLength: 0,
                    success: function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.nombre, value: item.nombre, id: item.id };
                            }));
                    }
                });
            },
            select: function (event, ui) {
                $("#PedidoDetalle_plato_id_fk").val(ui.item.id);
            }
        })
        .focus(function () {
            $(this).autocomplete("search", " ");
        });
});