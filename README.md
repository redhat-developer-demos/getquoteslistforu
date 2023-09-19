# getquoteslistforu
Part of the Labels Sandbox Activity

oc new-app https://github.com/donschenck/getquoteslistforu --labels=app.kubernetes.io/part-of=quotesforu,systemname=quotesforu,tier=dataaccess,language=csharp,quotesforu=dataaccess --image-stream="openshift/dotnet:7.0-ubi8" -e MONGODB_URI=mongodb://quote:quote@quote/quote
