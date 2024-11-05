// Write your JavaScript code.
var ActionsUI = {
    init: function (pAction) {
        $("*[data-disabledui_" + pAction + "]").attr("disabled", "disabled");
        $("*[data-readonlyui_" + pAction + "]").attr("readonly", "readonly");
    },
    create: function (pAction, pFunc) {
        if (pAction == "crear") {
            pFunc();
        }
    },
    edit: function (pAction, pFunc) {
        if (pAction == "modificar") {
            pFunc();
        }
    },
    delete: function (pAction, pFunc) {
        if (pAction == "eliminar") {
            pFunc();
        }
    },
    detail: function (pAction, pFunc) {
        if (pAction == "ver") {
            pFunc();
        }
    },
    allEx: function (pAction, pActionEx, pFunc) {
        if (pAction != pActionEx) {
            pFunc();
        }
    }
}