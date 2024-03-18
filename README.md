# geoPagos
Challenge geopagos
OBSERVACIONES:
En cuanto al tema de Logs, pude usar librerías como log4net, pero preferí hacerlo de manera manual para mostrar más código.la app directamente levanta una página de swagger donde pueden probar todos los llamados y donde se visualiza la tabla(se ve el mismo log que se guarda. que lo formatié para que sea una tabla)en cuanto a los logs generales y el de autorizaciones aprobadas, están en la carpeta temp de la máquina donde lo prueben( ya que no conocía algún directorio específico y de preferencia).Dichos logs se llaman "log.txt" y el que solo registra las autorizaciones finalizadas aprobadas, "finalLog.txt"
Respecto al punto opcional de dockerizar la api,  la solución ya cuenta con la imagen de docker. restarian los siguientes pasos:
1- Desde la carpeta donde está la api abrir una ventana de cmd.
2- correr el siguiente comando para crear el build
docker build -t paymentsapi .

3- logueados en docker, pusheamos la imagen con el siguiente comando (reemplazar "haylanpa" por sú usuario):
docker tag paymentsapi haylanpa/paymentsapi:v1
docker push haylanpa/paymentsapi:v1

4- Desde docker podemos hacerlo manual o bien, usando la terminal, resta hacer el deploy de la imagen. A continuación los comandos para hacerlo por terminal:estando logueados, nos traemos la imagen con el siguiente comando:
docker pull haylanpa/paymentsapi:v1
luego, lo corremos
:docker run -d -p 8080:80 --name paymentsapi haylanpa/paymentsapi:v1

NOTAS: en los ejemplos aparece mi usuario, el nombre que le dí a la imagen y el puerto para desplegar, modificar dichos parámetros a necesidad.
