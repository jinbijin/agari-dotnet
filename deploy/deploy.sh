dotnet publish -c Release --runtime ubuntu.18.04-x64 -o /tmp/AgariWebApi
ssh jinbijin@84.22.102.178 'pkill -x WebApi'
rsync -r --delete-after --quiet /tmp/AgariWebApi/ jinbijin@84.22.102.178:~/AgariWebApi
ssh -f jinbijin@84.22.102.178 './AgariWebApi/WebApi'
echo 'Deployment successful!'