# getquoteslistforu

## Part of the Red Hat Developer learning path entitled "Using Red Hat OpenShift labels"


oc new-app https://github.com/redhat-developer-demos/getquoteslistforu --labels=app.kubernetes.io/part-of=quotesforu,systemname=quotesforu,tier=dataaccess,language=csharp,quotesforu=dataaccess --image-stream="openshift/dotnet:7.0-ubi8" -e MONGODB_URI=mongodb://quote:quote@quote/quote
