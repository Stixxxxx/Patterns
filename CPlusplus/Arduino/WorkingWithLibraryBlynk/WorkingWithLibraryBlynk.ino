#include "DHT.h" // подключаем библиотеку для датчика
DHT dht(D2, DHT11); // сообщаем на каком порту будет датчик
#include <ArduinoJson.h>
#define BLYNK_PRINT Serial
#include <ESP8266WiFi.h>
#include <BlynkSimpleEsp8266.h>



// You should get Auth Token in the Blynk App.
// Go to the Project Settings (nut icon).
char auth[] = " ";

// Your WiFi credentials.
// Set password to "" for open networks.
char ssid[] = " ix";
char pass[] = " ";

int lastTime = 0;
int lastTimeStateMotionSensor = 0;


float Humidity;
float Temperature;

int humidityFromOnpenWeather;
float tempCFromOnpenWeather;

String line; 
const char* host = "api.openweathermap.org";

int stateMotionSensor;
int stateLed;

bool starter = false;

BLYNK_READ(V1) //Blynk app has something on V5
{  
  Blynk.virtualWrite(V1, Temperature); //sending to Blynk
}

BLYNK_READ(V2) //Blynk app has something on V5
{  
  Blynk.virtualWrite(V2, Humidity); //sending to Blynk
}

BLYNK_READ(V3) //Blynk app has something on V5
{  
  Blynk.virtualWrite(V3, tempCFromOnpenWeather); //sending to Blynk
}

BLYNK_READ(V4) //Blynk app has something on V5
{  
  Blynk.virtualWrite(V4, humidityFromOnpenWeather); //sending to Blynk
}

BLYNK_WRITE(V5)
{
  int pinValue = param.asInt();
  Serial.print("V1 Slider value is: ");
  Serial.println(pinValue);
  if(param.asInt() == 1) {     // if Button sends 1
   digitalWrite(D3, HIGH);
   
  }
  if(param.asInt() == 0) {     // if Button sends 1
   digitalWrite(D3, LOW);
   
  } 
}

void setup()
{ 
  pinMode(D3, OUTPUT);
  pinMode(D7, INPUT);

  digitalWrite(D3, LOW);
  stateLed = 0;
    
  // Debug console
  Serial.begin(9600);

  Blynk.begin(auth, ssid, pass);   
  dht.begin(); // запускаем датчик влажности DHT11

   // Set WiFi to station mode and disconnect from an AP if it was previously connected
  WiFi.mode(WIFI_STA);
  WiFi.disconnect();
  WiFi.begin(ssid, pass);

  delay(500);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  jsonGet();
  getDataWeather(); 
}

void loop()
{
  
  Blynk.run();

  Humidity = dht.readHumidity();
  Temperature = dht.readTemperature();

   if((millis() - lastTime)>3600000)
   {
    jsonGet();
    getDataWeather();
    
    lastTime=millis();
   }
    
    stateMotionSensor = digitalRead(D7);
  //  Serial.println(stateMotionSensor ); 

  if (stateMotionSensor == 1 ){
    if((millis() - lastTimeStateMotionSensor)>60000 || millis()<60000)
   {
    Blynk.notify("Внимание!!! Движение!"); 
    lastTimeStateMotionSensor=millis();
    Serial.println("Сенд дата фром метод нотификаций");  
   }        
  }  

}

void switchLed(){


  }

void jsonGet() {
  
  // Use WiFiClient class to create TCP connections
  WiFiClient client;
  const int httpPort = 80;
  if (!client.connect(host, httpPort)) {
    Serial.println("connection failed");
    return;
  }
  
    client.println("GET /data/2.5/weather?id=496519&units=metric&appid=c5ba134efa510143f5193f7d8e1f5bc7 HTTP/1.1");
    client.println("Host: api.openweathermap.org");
    client.println("Connection: close");
    client.println();
 
  delay(1500);
  // Read all the lines of the reply from server and print them to Serial
  while(client.available()){
    line = client.readStringUntil('\r'); 
  }
  Serial.print(line);
  Serial.println();
  Serial.println("closing connection");
}

void getDataWeather(){
   StaticJsonBuffer<2000> jsonBuffer;                   /// буфер на 2000 символов
   JsonObject& root = jsonBuffer.parseObject(line);     // скармиваем String
   if (!root.success()) {
    Serial.println("parseObject() failed");             // если ошибка, сообщаем об этом
     jsonGet();                                         // пинаем сервер еще раз
    return;                                             // и запускаем заного 
  }
                              /// отправка в Serial
  Serial.println();  
  String name = root["name"];                           // достаем имя, 
  Serial.print("name:");
  Serial.println(name);  
  
  tempCFromOnpenWeather = root["main"]["temp"];                   // достаем температуру из структуры main
  
  Serial.print("temp: ");
  Serial.print(tempCFromOnpenWeather);                                  // отправляем значение в сериал
  Serial.println(" C");

  float tempCmin = root["main"]["temp_min"];            // и так далее
  
  Serial.print("temp min: ");
  Serial.print(tempCmin);
  Serial.println(" C");

  float tempCmax = root["main"]["temp_max"];
  
  Serial.print("temp max: ");
  Serial.print(tempCmax);
  Serial.println(" C");
  
  int pressurehPa = root["main"]["pressure"]; 
  float pressure = pressurehPa/1.333;
  Serial.print("pressure: ");
  Serial.print(pressure);
  Serial.println(" mmHc");

  humidityFromOnpenWeather = root["main"]["humidity"]; 
  Serial.print("humidity: ");
  Serial.print(humidityFromOnpenWeather);  
  Serial.println(" %");   

  float windspeed = root["wind"]["speed"]; 
  Serial.print("wind speed: ");
  Serial.print(windspeed);  
  Serial.println(" m/s");

  int winddeg = root["wind"]["deg"]; 
  Serial.print("wind deg :");
  Serial.println(winddeg);  

   
  
 
  Serial.println();  
  Serial.println();  
 
  
  }
