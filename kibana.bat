docker stop kibana

docker rm kibana

docker run -d --name kibana -e ELASTICSEARCH_HOSTS=http://elasticsearch:9200/ -p 5601:5601 kibana:7.5.1