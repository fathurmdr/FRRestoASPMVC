/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Views/**/*.cshtml", "./wwwroot/**/*.js"],
    theme: {
        extend: {
            colors: {
                primary: "#601201"
            }
        },
    },
    plugins: [],
}

