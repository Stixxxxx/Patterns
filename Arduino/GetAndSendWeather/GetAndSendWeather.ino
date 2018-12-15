#include "DHT.h" // подключаем библиотеку для датчика
DHT dht(D2, DHT11); // сообщаем на каком порту будет датчик


#include <LiquidCrystal_I2C.h>
#include <Wire.h>


#include <ArduinoJson.h> 
#include <ESP8266WiFi.h>
 


const char* ssid     = "Stix";
const char* password = "SETDOMA1";

const char* host = "api.openweathermap.org";
String line; 
String dataTempHum;

LiquidCrystal_I2C lcd(0x3F, 16, 2);
int lastTime = 0;

void setup() {

  lcd.init();
  lcd.begin(16, 2);
  lcd.print("Hello!");
  lcd.backlight();

 dht.begin(); // запускаем датчик влажности DHT11
  
  Serial.begin(115200);
  delay(100);
   
  // We start by connecting to a WiFi network

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);


  
  // Set WiFi to station mode and disconnect from an AP if it was previously connected
  WiFi.mode(WIFI_STA);
  WiFi.disconnect();
  
  WiFi.begin(ssid, password);

  delay(500);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
    lcd.setCursor(0, 0);
    lcd.print("Connecting" );
    lcd.setCursor(0, 1);
    lcd.print("...");
  
  }

  Serial.println("");
  Serial.println("WiFi connected");  
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  lcd.setCursor(0, 0);
  lcd.print("WiFi. Your ip:");
  lcd.setCursor(0, 1);
  lcd.print(WiFi.localIP());
  
    jsonGet();
    getDataWeather();
    sendDataWeather();
  
}
 
void loop() {
  //3600000
   if((millis() - lastTime)>3600000)
   {
    getDataWeather();
    sendDataWeather();
    
    lastTime=millis();
   }
   
  
  
  
}

void sendDataWeather(){
   
  float h = dht.readHumidity();
  float t = dht.readTemperature();

   // выводим температуру (t) и влажность (h) на монитор порта

   Serial.print("Humidity: ");
   Serial.print(h);
   Serial.print(" %\t");
   Serial.print("Temperature: ");
   Serial.print(t);
   Serial.println(" *C");


  dataTempHum = "Humidity_"+String(h)+"%"+"Temperature_"+String(t)+"C";
  Serial.println(dataTempHum);
  sendRequest(dataTempHum);
  
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
  
  float tempC = root["main"]["temp"];                   // достаем температуру из структуры main
  
  Serial.print("temp: ");
  Serial.print(tempC);                                  // отправляем значение в сериал
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

  int humidity = root["main"]["humidity"]; 
  Serial.print("humidity: ");
  Serial.print(humidity);  
  Serial.println(" %");   

  float windspeed = root["wind"]["speed"]; 
  Serial.print("wind speed: ");
  Serial.print(windspeed);  
  Serial.println(" m/s");

  int winddeg = root["wind"]["deg"]; 
  Serial.print("wind deg :");
  Serial.println(winddeg);  

    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("temp: " + String(tempC) + " C");
    lcd.setCursor(0, 1);
    lcd.print("humidity: " + String(humidity) + " %");
  
 
  Serial.println();  
  Serial.println();  
 
  
  }

void sendRequest(String message) {
  
  WiFiClient client;
  const int httpPort = 80;
  if (!client.connect("maker.ifttt.com", httpPort)) {
    Serial.println("connection failed");
    return;
  }  
    client.println("GET /trigger/Arduino/with/key/cD6oVdcztDelUUBnS87Ngg?value1="+message+" HTTP/1.1");
    client.println("Host: maker.ifttt.com");
    client.println("Connection: close");
    client.println();
    
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
