@app.route('/itau')
def itau():
    result = tweetByTagAndDate("#itau", '2022-05-01T00:00:00Z','2022-05-08T20:00:00Z')
    return render_template("listTweetByTag.html", result=result)