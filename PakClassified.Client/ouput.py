import requests
from flask import Flask, jsonify
from bs4 import BeautifulSoup
import os
 
def extract():
    url = 'https://pbte.edu.pk/result.aspx'
    apikey = '3eda5c89e7981954492cf01ca8eb4943b976c671'
    params = {
        'url': url,
        'apikey': apikey,
        'js_render': 'true',
        'premium_proxy': 'true',
        'proxy_country': 'pk'
    }
    response = requests.get('https://api.zenrows.com/v1/', params=params)
    with open("output.html", "w", encoding="utf-8") as f:
        f.write(response.text)
    print("Success")


app = Flask(__name__)
app.config["JSONIFY_PRETTYPRINT_REGULAR"] = True  # 🔹 Enables formatted JSON output

ZENROWS_API_KEY = "3eda5c89e7981954492cf01ca8eb4943b976c671"  # Replace with your ZenRows key

@app.route("/pbte_result", methods=["GET"])
def get_courses():
    try:
        extract()
        print("HTML Extracted by ZenRows;")

        # 2️⃣ Read saved HTML and parse
        with open("output.html", "r", encoding="utf-8") as f:
            html_content = f.read()

        soup = BeautifulSoup(html_content, "html.parser")
        select_tag = soup.find("select", id="cmbcat")
        if not select_tag:
            return jsonify({"error": "Course dropdown not found"}), 404

        courses_dict = {
            opt["value"]: opt.get_text(strip=True)
            for opt in select_tag.find_all("option")
        }
        courses_list = list(courses_dict.values())

        result_status = (
            "Your result has been announced!"
            if any("DAE" in c for c in courses_list)
            else "Your Result is NOT announced yet!"
        )

        return jsonify({
            "courses_dict": courses_dict,
            "courses_list": courses_list,
            "total_courses": len(courses_list),
            "result_status": result_status,
            "html_saved_to": os.path.abspath("output.html")
        })

    except Exception as e:
        return jsonify({"error": str(e)}), 500


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8080)
