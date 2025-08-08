import { form, register } from "../Features/Register.js"

form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await register(form);
    if (res)
        window.location.href = "/Feed";
    else {
        //Se cambiará para mostrar mensaje de error
        window.location.href = "/Auth/Forbidden";
    }
})