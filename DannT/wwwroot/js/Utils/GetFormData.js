export function formToFormData(form) {
    return new FormData(form);
}
export function formToObject(form) {
    let formData = formToFormData(form)
    return Object.fromEntries(formData.entries());
}
