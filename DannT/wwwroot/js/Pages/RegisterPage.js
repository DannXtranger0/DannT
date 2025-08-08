import { form, login} from "../Features/Login.js"

form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await login(form);
    if (res)
        window.location.href = "/Feed";
    else {
        //Se cambiará para mostrar mensaje de error
        window.location.href = "/Auth/Forbidden";
    }
})