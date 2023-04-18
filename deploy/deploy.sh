dotnet publish WebApi/WebApi.csproj -c Release --os linux -o /tmp/AgariWebApi --self-contained
ssh jinbijin@84.22.102.178 'pkill -x WebApi'
rsync -r --delete-after --quiet /tmp/AgariWebApi/ jinbijin@84.22.102.178:~/AgariWebApi
ssh -f jinbijin@84.22.102.178 './AgariWebApi/WebApi'
echo 'Deployment successful!'