#!/bin/bash
docker push pitstop/customermanagementapi:latest
docker push pitstop/customermanagementapi:v1 
docker push pitstop/customermanagementapi/v2
docker push pitstop/webapp:latest
docker push pitstop/workshopmanagementeventhandler:latest
docker push pitstop/timeservice:latest
docker push pitstop/notificationservice:latest
docker push pitstop/invoiceservice:latest
docker push pitstop/auditlogservice:latest
docker push pitstop/workshopmanagementapi:latest
docker push pitstop/vehiclemanagementapi:latest