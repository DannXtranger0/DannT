import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

let buttonLogin = document.getElementById("LoginGoogle");

buttonLogin.addEventListener("click", () => {
    window.location.href = `${localDomain}api/AuthApi/GoogleLogin`;
})