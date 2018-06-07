var DatatableHtmlTableDemo = {
    init: function () {
        var e; e = $(".m-datatable").
            mDatatable({
                data: { saveState: { cookie: false } },
                search: { input: $("#generalSearch") },
                layout: { theme: "default", "class": "", scroll: false, footer: false },
                columns: [
                    { field: "ID", type: "number", width: 20, sortable: "asc" },
                    { field: "Acciones", width: 255, overflow: "visible"}
                ]
            }), $("#m_form_status").on("change", function () { e.search($(this).val().toLowerCase(), "Status") }), $("#m_form_type").on("change", function () { e.search($(this).val().toLowerCase(), "Type") }), $("#m_form_status, #m_form_type").selectpicker();
    }
}; jQuery(document).ready(function () { DatatableHtmlTableDemo.init() });