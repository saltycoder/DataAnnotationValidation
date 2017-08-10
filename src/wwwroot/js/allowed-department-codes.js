$.validator.unobtrusive.adapters.addSingleVal("departmentcode", "allowed");

$.validator.addMethod("departmentcode", function (value, element, allowed) {
    if (value) {
        var codes = allowed.split(",");

        if (jQuery.inArray(value.toUpperCase(), codes) == -1) {
            return false;
        }
    }
    return true;
});