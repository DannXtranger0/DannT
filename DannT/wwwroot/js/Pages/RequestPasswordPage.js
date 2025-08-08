import { form,savePassword } from "../Features/RequestPassword.js"

form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await savePassword(form);
    console.log(res);
    if (res)
        window.location.href = "/Feed";
    else {
        window.location.href = "/Auth/RequestPassword";
    }
})