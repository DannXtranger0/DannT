export async function getJson(url, options = {}) {
    try {
        let response = await fetch(url, options);
        if (!response.ok)
            throw new Error(`Response status: ${response.status}`);

        let data = await  response.json();
        console.log(data);
        return data;
    } catch (err) {
        console.log(err.message);
    }
    
}