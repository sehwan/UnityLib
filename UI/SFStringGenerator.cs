using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using System.Text;

public class SFStringGenerator : MonoBehaviour
{
  Text txt;
  public float lineChance = 1f;
  public int lineLength = 100;
  public int min = 300;
  public int max = 2000;
  public float interval = 0.05f;
  public float cool = 5f;


  void Awake()
  {
    txt = GetComponent<Text>();
    txt.text = str.GetRandomSubstring(RandomEx.R(max, min));
  }
  private void OnEnable()
  {
    StartCoroutine(Generate());
  }
  IEnumerator Generate()
  {
    while (true)
    {
      var len = Random.Range(min, max);
      var div = Random.Range(1, 10);
      var sb = new StringBuilder();
      for (int i = 0; i < div; i++) sb.AppendLine(str.GetRandomSubstring(len / div));
      txt.DOText(sb.ToString(), len * interval, true, ScrambleMode.None).SetEase(Ease.Linear).SetUpdate(true);
      yield return new WaitForSeconds(cool + interval * len);
      txt.text = "";
    }
  }

  readonly string str = @"
import pyqtconsole.highlighter as hl
console = PythonConsole(formats={
    'keyword':    hl.format('blue', 'bold'),
    'operator':   hl.format('red'),
    'brace':      hl.format('darkGray'),
    'defclass':   hl.format('black', 'bold'),
    'string':     hl.format('magenta'),
    'string2':    hl.format('darkMagenta'),
    'comment':    hl.format('darkGreen', 'italic'),
    'self':       hl.format('black', 'italic'),
    'numbers':    hl.format('brown'),
    'inprompt':   hl.format('darkBlue', 'bold'),
    'outprompt':  hl.format('darkRed', 'bold'),
})

88888888888 888                                                       
    888     888                                                       
    888     888                                                       
    888     88888b.  888d888 .d88b.   .d88b.                          
    888     888  88b 888P   d8P  Y8b d8P  Y8b                         
    888     888  888 888    88888888 88888888                         
    888     888  888 888    Y8b.     Y8b.                             
    888     888  888 888      Y8888    Y8888                          
                                                                      
                                                                      
                                                                      
8888888888 d8b                                                        
888        Y8P                                                        
888                                                                   
8888888    888 88888b.   .d88b.   .d88b.  888d888 .d8888b             
888        888 888  88b d88P 88b d8P  Y8b 888P    88K                 
888        888 888  888 888  888 88888888 888      Y8888b.            
888        888 888  888 Y88b 888 Y8b.     888          X88            
888        888 888  888   Y88888   Y8888  888      88888P'            
                             888                                      
                        Y8b d88P                                      
                          Y88P                                        
888      d8b 888                               888                    
888      Y8P 888                               888                    
888          888                               888                    
888      888 88888b.   .d88b.  888d888 8888b.  888888 .d88b.  888d888 
888      888 888  88b d8P  Y8b 888P        88b 888   d88  88b 888P    
888      888 888  888 88888888 888    .d888888 888   888  888 888     
888      888 888 d88P Y8b.     888    888  888 Y88b. Y88..88P 888     
88888888 888 88888P     Y8888  888     Y888888   Y888  Y88P   888     

Alchemists (hereinafter referred to as the Company) collects only the minimum personal information necessary to use the services provided by the Company. Through this Privacy Policy, we inform users about the types of personal information collected by the Company, its purposes, recipients, and methods of disposal. This policy also applies to additional services directly or indirectly related to game services.
Collection of Personal Information
Required Information: Basic personal information collected to provide essential services.
Optional Information: Additional personal information collected to provide supplementary services. We collect the following personal information through web pages, apps, emails, written forms, etc.:
Mobile game services without a login process: [Required] ID, game usage data, Device ID
Mobile game services with a login process: [Required] ID, password, game usage data, Device ID
During the process of using the services for purposes such as ensuring service stability, preventing unauthorized use, protecting accounts and items, and restricting actions that violate laws and company terms of service, automatically generated information (IP, connection records, service usage records, improper usage records, download records, payment records, cookies, etc.) and information that can identify devices (OS information, hardware information, Mac Address, advertising identifiers, etc.) may be collected. When using the Company's services with an account from another company, only the personal information consented by the user and within the agreed-upon scope will be provided.
Use of Personal Information
Personal information is used to identify users, confirm their identities, and manage user accounts.
For children under the age of 14, personal information is used to verify the legal guardian's identity and confirm their consent.
Personal information is used to provide mobile game services, additional services, payments, and refunds.
Personal information is used to inform users who participated in pre-registration about mobile game downloads, releases, updates, and for promotional purposes.
Personal information is used to confirm participation in events, deliver prizes to event participants, and handle tax payments.
Personal information is used to notify users of changes in terms and conditions, service disruptions, game usage history, and personal information usage history related to service operations.
Personal information is used to respond to inquiries, handle complaints, and improve user services.
Personal information is used to create a secure environment for using the services.
Personal information is used to analyze service usage environments through service usage records, in order to improve services and provide services tailored to user characteristics.
Provision of Personal Information To provide users with more convenient and prompt services, the Company entrusts certain tasks to other companies. During the process of using services, automatically generated information (IP, connection records, service usage records, improper usage records, download records, payment records, cookies, etc.) and device information (OS information, hardware information, Mac Address, advertising identifiers, etc.) may be provided for the purposes of ensuring service stability, preventing unauthorized use, and protecting accounts.
Disposal of Personal Information The Company promptly disposes of personal information when the purpose of collection and use is achieved. However, in cases where it is stipulated by related laws or consent has been obtained from the user, exceptions may apply.
Act on Consumer Protection in Electronic Commerce, Etc. Records related to contracts or withdrawal of subscription: 5 years Records related to payment and supply of goods, etc.: 5 years Records related to consumer complaints or dispute resolution: 3 years For users who withdraw from the game, the information collected for service provision will be stored for 30 days after the withdrawal date for the purpose of resolving consumer complaints and disputes before being deleted. Electronically stored personal information will be deleted, and personal information printed on paper will be shredded.
Rights of Users and Legal Guardians and How to Exercise Them The Company actively takes necessary measures to accommodate users' requests for accessing, providing, and correcting personal information. Legal guardians of children under the age of 14 have the right to access, provide, and correct their child's personal information. Requests for accessing, providing, correcting, or revoking consent for personal information can be made through phone calls, emails, etc.
Installation, Operation, and Rejection of Automatic Personal Information Collection Devices Online personalized advertising providers are allowed to collect behavioral information.
Online personalized advertising: Marketing techniques that provide services tailored to the user's characteristics by analyzing the user's online activities, connection records, etc.
Advertising providers: Google, Facebook, Twitter, Admob, UnitAds, Vungle
Method of collecting behavioral information: Automatically collected when the app is launched by the user.
Limitation of Service Use If a user refuses the Company's collection of personal information, there may be limitations on the use of all or some of the game services provided by the Company.
Collecting Company and Contact Information
Company Name: Alchemists
Email: alchemistsgames@gmail.com
In the event of any changes, we will notify you through the Privacy Policy.
August 1, 2019

                                                                      
npm ERR! code EUSAGE
npm ERR! 
npm ERR! Manipulates packages cache
npm ERR! 
npm ERR! Usage:
npm ERR! npm cache add <package-spec>
npm ERR! npm cache clean [<key>]
npm ERR! npm cache ls [<name>@<version>]
npm ERR! npm cache verify
npm ERR! 
npm ERR! Options:
npm ERR! [--cache <cache>]
npm ERR! 

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_48_33_064Z-debug-0.log

Terminal messages are used to end communication round-trips between client and server. There are three terminal-message types:

Terminal 1 messages:
This is the most straightforward message-termination method. Terminal 1 messages are always initiated by the client. An example is the client asking the server, “The user just pressed this button, what should I show in the UI?” The server then responds with the requested answer and terminates the exchange with a Terminal 1 message.
Terminal 2 messages:
Sometimes, communication between client and server is more complicated. For example, if the client says, “The user just pressed this button, what should I show in the UI?” but the server needs more information from the client before it can provide an answer. The server responds with a Terminal 2 message, asking something like, “What is the state of option button XYZ?” If the client has a prepared, scripted response for this request, it will provide the answer to the server’s question and end communication with a Terminal 2 message. The server will then provide a scripted response to the client’s original question and end the communication with a Terminal 1 message.
This type of exchange can be illustrated as follows:
Request from client (Terminal 1, Out Body tab)
Request from server (Terminal 2, In Body tab)
Response from client (Terminal 2, Out Body tab)
Response from server (Terminal 1, In Body tab)

// Download the helper library from https://www.twilio.com/docs/node/install
// Find your Account SID and Auth Token at twilio.com/console
// and set the environment variables. See http://twil.io/secure
const accountSid = process.env.TWILIO_ACCOUNT_SID;
const authToken = process.env.TWILIO_AUTH_TOKEN;
const client = require('twilio')(accountSid, authToken);
---------------------------------------------------
npm ERR! code ENEEDAUTH
npm ERR! need auth This command requires you to be logged in.
npm ERR! need auth You need to authorize this machine using `npm adduser`

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_48_07_829Z-debug-0.log

client.messages
  .create({
     body: 'This is the ship that made the Kessel Run in fourteen parsecs?',
     from: '+15017122661',
     to: '+15558675310'
   })
  .then(message => console.log(message.sid));
  
  Running the Script
To run the script, make sure you are running in a stand-alone terminal instead of in the terminal of your IDE.

cd src/gui_textual
pipenv install
pipenv run python tui.py
Copy
Text-based User Interface
A Text-based User Interface (TUI) is similar to a Graphical User Interface (GUI), except that the elements are rendered using ASCII text directly in the terminal. Textual is a Python package that provides a Rapid Application Development framework that enables a very simple example that is a step up from Command Line Interface (CLI).

=======================================================
const styleguide = require('@nurun-sf/spark-style-guide');

// Configure style guide
styleguide.configure({
  name: 'Spark Sassplate',
  client: 'Odopod',
  jsonSource: 'extensions',
  stylesheets: [
    'styles.css',
    'css/docs.css',
  ],
  dist: {
    markup: path.join(process.cwd(), 'dist'),
  },
});

 _____   _                                 
|_   ___| |__ _ ___  ___                   
  | |/ _` |__` / _ \/ _ \                  
  | | | | |  | \__  \__  |                 
  |_|_| |_|  |_|___/|___/                  
                                           
 _____ _                                   
|___  (_) __ _ _ __  ___ __ _ ___          
   _| | |/ _` | '_ \/ _ |__` |__ \         
  |_  | | | | | |_) \__  | | / __/         
    |_|_|_| |_| .__/|___/  |_\___|         
               \___|                       
     _ _     _                 _           
    | (_) __| |___ __ _ _ __ _| | ___ __ _ 
    | | |/ _` / _ |__` | '_ |__ |/ _ |__` |
 ___| | | (_| \__  | | | |_) _| | (_) | | |
|_____|_|\__,_|___/  |_|_.__|__/ \___/  |_|
                                           

###############################################################
[x] Design, animations and sounds guidelines
[x] Playground for development
[x] Base API for components and tools
[x] Support for server-side rendering
[x] Define logo
[x] Documentation
[x] Create component API docs
[x] Create website playground
[x] Create resources to learn JSS / React
[ ] Refactor project architecture (In Progress)
[ ] Standardize core components (In Progress)
[ ] Add form components
[ ] Add navigation components
[ ] Best test coverege
==================================================================
조회수 11,283회  2019. 9. 5.
COSMIC PSYCHO
コズミックサイコ
npm ERR! code EUSAGE
npm ERR! 
npm ERR! Manage the npm configuration files
npm ERR! 
npm ERR! Usage:
npm ERR! npm config set <key>=<value> [<key>=<value> ...]
npm ERR! npm config get [<key> [<key> ...]]
npm ERR! npm config delete <key> [<key> ...]
npm ERR! npm config list [--json]
npm ERR! npm config edit
npm ERR! 
npm ERR! Options:
npm ERR! [--json] [-g|--global] [--editor <editor>] [-L|--location <global|user|project>]
npm ERR! [-l|--long]
npm ERR! 
npm ERR! alias: c
npm ERR! 

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_48_55_907Z-debug-0.log
const val = invariantNumberFormat(100123);
Expect(localeNumberFormat(val)).toEqual(localeNumberFormat({
}))
01 DEEP SPACE 
02 NEPTUNE
03 MEMORY
04 STG 1
05 LIBERATION~BEGIN
06 DEEP SPACE
07 COSMIC
08 NEPTUNE
09 ENEMY APPROACHING
10 STG 1
11 BAR
12 SEXY
13 ICE BREAKER
14 STG 5
15 STG 2
16 JUMP OUT
17 SCREAMING FAUST
18 STG 4
19 KILLING MACHINE
20 SETTLE~SAY GOOD BYE
21 SCHOOL
22 SWEETHEART
23 REGRETS
24 MEMORY
25 AGAIN
26 NIGHT WALKER
27 EX…
28 STG 3
29 GAME OVER
30 SADNESS
31 DARKNESS
32 LIBERATION
33 BEGIN

# List of finder classes that know how to find static files in
# various locations.
STATICFILES_FINDERS = (
    'django.contrib.staticfiles.finders.FileSystemFinder',
    'django.contrib.staticfiles.finders.AppDirectoriesFinder',
#    'django.contrib.staticfiles.finders.DefaultStorageFinder',
)

# Make this unique, and don't share it with anybody.
SECRET_KEY = '9a7!^gp8ojyk-^^d@*whuw!0rml+r+uaie4ur$(do9zz_6!hy0'

# List of callables that know how to import templates from various sources.
TEMPLATE_LOADERS = (
    'django.template.loaders.filesystem.Loader',
    'django.template.loaders.app_directories.Loader',
#     'django.template.loaders.eggs.Loader',
)
Arranged version: 1~5
X68000 version: 6, 7, 20~31
PC9801 version: 8~19, 32, 33

6. Create a new branch.

git checkout -b <your_branch_name>
7. Add & Commit your changes.

  git add .
  starCount	Total number of stars ( high value affect performance )	number	150
rotationSpeed	The rotation speed of stars ( high value affect performance )	number	0.01
minSize	Star's minimum size for our randomizer (⚠️ ] 0 ; 2 [ )	number	0.5
maxSize	Star's maximum size for our randomizer (⚠️ ] 0 ; 2 [ && > minSize )	number	0.5
opacity	Galaxy Backgound opacity	number	0.9
bgColor	Background color	string (Hex Code)	#000000
starColor	Stars color	string (Hex Code)	#ffffff
innerRadius	Size of the empty star area	number	100


 dMMMMMMP dMP dMP dMMMMb  dMMMMMP dMMMMMP                              
   dMP   dMP dMP dMP.dMP dMP     dMP                                   
  dMP   dMMMMMP dMMMMK  dMMMP   dMMMP                                  
 dMP   dMP dMP dMP AMF dMP     dMP                                     
dMP   dMP dMP dMP dMP dMMMMMP dMMMMMP                                  
                                                                       
    dMMMMMP dMP dMMMMb  .aMMMMP dMMMMMP dMMMMb  .dMMMb                 
   dMP     amr dMP dMP dMP     dMP     dMP.dMP dMP  VP                 
  dMMMP   dMP dMP dMP dMP MMP dMMMP   dMMMMK   VMMMb                   
 dMP     dMP dMP dMP dMP.dMP dMP     dMP AMF dP .dMP                   
dMP     dMP dMP dMP  VMMMP  dMMMMMP dMP dMP  VMMMP                     
                                                                       
    dMP     dMP dMMMMb  dMMMMMP dMMMMb  .aMMMb dMMMMMMP .aMMMb  dMMMMb 
   dMP     amr dMP dMP dMP     dMP.dMP dMP dMP   dMP   dMP dMP dMP.dMP 
  dMP     dMP dMMMMK  dMMMP   dMMMMK  dMMMMMP   dMP   dMP dMP dMMMMK   
 dMP     dMP dMP.aMF dMP     dMP AMF dMP dMP   dMP   dMP.aMP dMP AMF   
dMMMMMP dMP dMMMMP  dMMMMMP dMP dMP dMP dMP   dMP    VMMMP  dMP dMP    
                                                                       

Obfuscation tool for .NET assemblies
Obfuscar is a basic obfuscator for .NET assemblies. It uses massive overloading to rename metadata in .NET assemblies (including the names of methods, properties, events, fields, types and namespaces) to a minimal set, distinguishable in most cases only by signature.

For example, if a class contains only methods that accept different parameters, they can all be renamed 'A'. If another method is added to the class that accepts the same parameters as an existing method, it could be named 'a'.

It makes decompiled code very difficult to follow. The wiki has more details about WhatItDoes.

The current stable release is Obfuscar 1.5.4.

There is also the Obfuscar 2.0.0 Beta release. This is a port of Obfuscar 1.5.4 to the new Mono.Cecil 0.9 library. By use of this new library Obfuscar now supports .NET 4.0 assemblies. Because there are a lot of subtle changes in Cecil's new API this release of Obfuscar must be considered beta.

Note: Since version 1.5 the attrib attribute is evaluated correctly. Be sure to check if there are any unintended attrib values from the example in your configuration file.

Obfuscar works its magic with the help of Jb Evain's fantastic Cecil library, and uses the C5 Generic Collection Library to hold its data.
#ifdef GL_ES
precision mediump float; 
#endif
npm ERR! code ENOENT
npm ERR! syscall open
npm ERR! path /Users/sehwanlim/package.json
npm ERR! errno -2
npm ERR! enoent ENOENT: no such file or directory, open '/Users/sehwanlim/package.json'
npm ERR! enoent This is related to npm not being able to find a file.
npm ERR! enoent 

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_47_41_726Z-debug-0.log
sehwanlim@SEui-MacBookAir ~ % 



uniform float 		time;
uniform vec2 		mouse;
uniform vec2 		resolution;
uniform sampler2D 	renderbuffer;

#define segments 6
#define reads (segments * 2)
#define writes (segments * 2 + 1)
#define tilesize 8.

struct controller2d{
	vec2 t, v, e, i, d;
};

INSTALLED_APPS = (
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.sites',
    'django.contrib.messages',
    'django.contrib.staticfiles',
    # Uncomment the next line to enable the admin:
    'django.contrib.admin',
    # Uncomment the next line to enable admin documentation:
    # 'django.contrib.admindocs',
    'zdm',
    'portal',
    'admin',
    'tagging',
)
void pid(inout controller2d c);

float	line(vec2 p, vec2 a, vec2 b, float w);
float	hash(float v);
vec2	toworld(vec2 p);
vec3	hsv(float h,float s,float v);

void	read(out vec4[reads] m);
void	write(const in vec4[writes] w);
vec4	compact(vec4 v);
vec4	expand(vec4 v);

void main( void ) {
	vec2 uv = gl_FragCoord.xy/resolution.xy;
	vec4 rb = texture2D(renderbuffer, uv);
	vec2 p	= toworld(uv);

	vec4 m[reads];
	read(m);		
	
	controller2d c[segments];
	
	vec3 l = vec3(0.);
	vec2 lv = vec2(0.);
	for(int i = 0; i < segments; i++)
	{
		vec4 mx	= expand(m[i]);
		vec4 my	= expand(m[segments+i]);
		
		c[i].t	= i > 0 ? lv : mouse;
		c[i].v 	= vec2(mx.x, my.x);
		c[i].e 	= vec2(mx.y, my.y);
		c[i].i 	= vec2(mx.z, my.z);
		c[i].d 	= vec2(mx.w, my.w);
	
		pid(c[i]);
	
		
		c[i].v 	= c[i].v - c[i].e - c[i].d * .025;
	
		lv 	= c[i].v;
		
		vec2 a  = toworld(c[i].v);
		vec2 b  = toworld(c[i].t);
		l 	= max(l, hsv(float(i)*.1, 1., 1.) * line(p, a, b, .095));
	}
	
	vec4 r 	= vec4(l, 1.);
	

	vec4 w[writes];
    	w[writes-1]	= r;
	for(int i = 0; i < segments; i++)
	{
		w[i]		= compact(vec4(c[i].v.x, c[i].e.x, c[i].i.x, c[i].d.x));	
		w[segments+i]	= compact(vec4(c[i].v.y, c[i].e.y, c[i].i.y, c[i].d.y));
	}
	write(w);
    
}//sphinx


#define kp .75
#define ki 1.
#define kd .015
#define ke 3.;
void pid(inout controller2d c)
{
	vec2 error 	= c.t - c.v;
	vec2 integral	= c.i + error/ke;
	vec2 delta	= error - c.e;

	c.v = kp * error + ki * integral + kd * delta;
	c.e = error;
	c.i = integral;
	c.d = delta;
}

float line(vec2 p, vec2 a, vec2 b, float w){
	if(a==b)return(0.);
	float d = distance(a, b);
	vec2  n = normalize(b - a);
    	vec2  l = vec2(0.);
	l.x = max(abs(dot(p - a, n.yx * vec2(-1.0, 1.0))), 0.0);
	l.y = max(abs(dot(p - a, n) - d * 0.5) - d * 0.5, 0.0);
	return smoothstep(w, 0., l.x+l.y);
}

____________________________________                         
7      77  7  77  _  77     77     7                         
!__  __!|  !  ||    _||  ___!|  ___!                         
  7  7  |     ||  _ \ |  __|_|  __|_                         
  |  |  |  7  ||  7  ||     7|     7                         
  !__!  !__!__!!__!__!!_____!!_____!                         
                                                             
______________________________________________               
7     77  77     77     77     77  _  77     7               
|  ___!|  ||  _  ||   __!|  ___!|    _||  ___!               
|  __| |  ||  7  ||  !  7|  __|_|  _ \ !__   7               
|  7   |  ||  |  ||     ||     7|  7  |7     |               
!__!   !__!!__!__!!_____!!_____!!__!__!!_____!               
                                                             
____   ______________________________________________________
7  7   7  77  _  77     77  _  77  _  77      77     77  _  7
|  |   |  ||   __||  ___!|    _||  _  |!__  __!|  7  ||    _|
|  !___|  ||  _  ||  __|_|  _ \ |  7  |  7  7  |  |  ||  _ \ 
|     7|  ||  7  ||     7|  7  ||  |  |  |  |  |  !  ||  7  |
!_____!!__!!_____!!_____!!__!__!!__!__!  !__!  !_____!!__!__!
                                                             

vec2 toworld(vec2 p){
	p = p * 2. - 1.;
	p.x *= resolution.x/resolution.y;
	return p;
}

vec3 hsv(float h,float s,float v){
	return mix(vec3(1.),clamp((abs(fract(h+vec3(3.,69.,3.)/4.)*6.-3.)-0.),0.,4.),s)*v;
}

vec4 expand(vec4 v)
{
	return (v*2.)-1.;	
}
	
vec4 compact(vec4 v)
{	
	v = clamp(v, -1., 1.);
	return (v + 1.)*.5;
}

void read(out vec4[reads] m)
{
	float px = 1./resolution.x;
	vec2 b = vec2(0., 0.);
	float t = 0.;
	float f = 0.;
	float h = 0.;
	float j = 0.;
	for(int i = 0; i < reads; i++)
	{
		j = float(i);
		t = floor(float(i)/tilesize);
		f = floor(float(i));
		h = floor(float(i)*.5)*2.;
		b.x = mod(f,2.)+h-t*tilesize;
		b.y = mod(b.x,2.)+t*2.;
		m[i] = texture2D(renderbuffer, (vec2(b.x, b.y)+1.)/resolution);
	}	
}
npm ERR! code ENOENT
npm ERR! syscall open
npm ERR! path /Users/sehwanlim/package.json
npm ERR! errno -2
npm ERR! enoent ENOENT: no such file or directory, open '/Users/sehwanlim/package.json'
npm ERR! enoent This is related to npm not being able to find a file.
npm ERR! enoent 

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_47_41_726Z-debug-0.log
sehwanlim@SEui-MacBookAir ~ % 



void write(const in vec4[writes] w)
{
	vec4 m 	= w[writes-1];
	vec2 b = vec2(0., 0.);
	vec2 fc = floor(gl_FragCoord.xy);
	float t = 0.;
	float f = 0.;
	float h = 0.;
	float j = 0.;
	if(gl_FragCoord.x<float(writes-1))
	{
		for(int i = 0; i < writes; i++)
		{
			j = float(i);
			t = floor(j/tilesize);
			f = floor(j);
			h = floor(j*.5)*2.;
			b.x = mod(f,2.)+h-t*tilesize;
			b.y = mod(b.x,2.)+t*2.;
			m = fc.x == b.x && fc.y == b.y ? w[i] : m;
		}
	}
	gl_FragColor = m;
}
#ifdef GL_ES
precision mediump float;
#endif

// added a little hack to effectively keep the thickness of the white lines constant in screen-space, by upscaling based on distance.

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

vec3 rotXY(vec3 p, vec2 rad) {
	vec2 s = sin(rad);
	vec2 c = cos(rad);
	
	mat3 m = mat3(
		c.y, 0.0, -s.y,
		-s.x * s.y, c.x, -s.x * c.y,
		c.x * s.y, s.x, c.x * c.y
	);
	return m * p;
}

vec2 repeat(vec2 p, float n) {
	vec2 np = p * n;
	vec2 npfrct = fract(np);
	vec2 npreal = np - npfrct;
	np.x += fract(npreal.y * 0.5);
	
	return fract(np) * 2.0 - 1.0;
}

float hexDistance(vec2 ip) {
	const float SQRT3 = 1.732050807568877;
	const vec2 TRIG30 = vec2(0.5, 0.866025403784439); //x:sine, y:cos
	
	vec2 p = abs(ip * vec2(SQRT3 * 0.5, 0.75));
	float d = dot(p, vec2(-TRIG30.x, TRIG30.y)) - SQRT3 * 0.25;
	
	return (d > 0.0)? min(d, (SQRT3 * 0.9 - p.x)) : min(-d, p.x);
}

float smoothEdge(float edge, float margin, float x) {
	return smoothstep(edge - margin, edge + margin, x);
}

void main(void) {
	const float PI = 3.1415926535;
	vec3 rgb;
	vec2 whatpos=vec2(0.65,-.0);
	vec2 nsc =1.* (gl_FragCoord.xy/resolution.xy-whatpos)*2.-1.;
	vec3 dir = normalize(vec3(nsc, -4.0));
	//dir = rotXY(dir, vec2((.5,1.25)));
	vec2 uv = vec2(atan(dir.y, dir.x) / (PI * 1.0) + 0.5, dir.z / length(dir.xy));
	
	vec2 pos = uv * vec2(2.,.2) - vec2(time * 0.05, time * 0.5);
	
	vec2 p = repeat(pos, 16.0);
	
	float d = hexDistance(p);
	float dist = dir.z/length(dir.xy);
	d/=-dist;
	float fade = 1.0 / pow(1.0 / length(dir.xy) * 0.3, 0.4);
	fade = clamp(fade, 0.0, 1.0);
	rgb  = mix(vec3(1.0)*fade, vec3(0.0), smoothEdge(0.03, 0.01, d));
	rgb += mix(vec3(1.0, 0.0, 1.0)*fade, vec3(0.0), smoothEdge(0.03, 0.5, d)) * 0.5;
	rgb += mix(vec3(1.0, 0.0, 0.0)*fade, vec3(0.0), smoothEdge(0.03, 1.0, d)) * 0.25;
	
	gl_FragColor = vec4(rgb, 1.0);
	
}
Caused by java.lang.IllegalAccessException: Class java.lang.Class<java.lang.reflect.Method> cannot access protected  method java.lang.Class com.appsflyer.internal.AFb1mSDK$$.loadClass(java.lang.String, boolean) of class java.lang.Class<com.appsflyer.internal.AFb1mSDK$$>
       at java.lang.reflect.Method.invokeNative(Method.java)
       at java.lang.reflect.Method.invoke(Method.java:423)
       at com.appsflyer.internal.AFb1mSDK$$.AFKeystoreWrapper(:100)
       at com.appsflyer.internal.AFb1mSDK$$.values(:19)
       at java.lang.reflect.Method.invokeNative(Method.java)
       at java.lang.reflect.Method.invoke(Method.java:423)
       at com.appsflyer.internal.AFb1pSDK.values(:127)
       at com.appsflyer.internal.AFd1eSDK.values(:9132)
       at com.appsflyer.internal.AFd1hSDK.values(:81)
       at com.appsflyer.internal.AFd1eSDK.AFKeystoreWrapper(:78)
       at com.appsflyer.internal.AFd1nSDK.AFInAppEventParameterName(:115)
       at com.appsflyer.internal.AFd1tSDK$5.run(:109)
       at java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1167)
       at java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:641)
       at java.lang.Thread.run(Thread.java:923)
#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

float plasma(vec2 p, float iso, float fade)
{
	float c = 0.0;
	for (float i=1.0; i<10.0; ++i) {
		float f1 = i / 0.6;
		float f2 = i / 0.3;
		float f3 = i / 0.7;
		float f4 = i / 0.5;
		float s1 = i / 2.0;
		float s2 = i / 4.0;
		float s3 = i / 3.0;
		c += sin(p.x * f1 + time) * s1 + sin(p.y * f2 + 0.0 * time) * s2 + sin(p.x * f3 + p.y * f4 - 1.5 * time) * s3;
	}
	//c = mod(clamp(c, -1.0, 1.0), 0.5) * 2.0;
	c = mod(c, 16.0) * 0.5 - 7.0;
	if (c < iso) {
		return 0.0;
	}
	else {
		if (c > 0.5) c = 1.0 - c;
		c *= 2.0;
		return c * fade;
	}
}
VALVE CORPORATION
NONDISCLOSURE AGREEMENT

THIS AGREEMENT (the “Agreement”) is made by and between VALVE CORPORATION (“VALVE”) a Washington corporation and the person or entity identified by you in the “Company Legal Name” field in connection with the Agreement completion process (“Company”), and is effective on the date that Valve provides you with confirmation of its acceptance.

In consideration of the promises and covenants contained in this Agreement, the parties hereto agree as follows: 

A.   CONFIDENTIAL INFORMATION
As used in this Agreement, “Confidential Information” means information which VALVE or its Affiliate(s) designate to Company as being confidential or which, based on the nature of such information and the circumstances surrounding its disclosure, ought in good faith to be treated as confidential.  Confidential Information includes, but is not limited to, any discussions and materials regarding unreleased Valve products between Valve, or its Affiliate(s), and the Company, and the terms of any Valve agreement or agreement template.  Confidential Information shall not, however, include information Company can conclusively establish: (i) has entered the public domain without Company’s breach of any obligation owed to VALVE; (ii) is rightfully received by Company from a third party without confidentiality restrictions; (iii) is known to Company without any restriction as to use or disclosure prior to first receipt by Company from VALVE; or (iv) is independently developed by Company.  An “Affiliate” of a party to this Agreement means any entity that directly or indirectly controls, is controlled by, or is under common control with VALVE.
B.   RESTRICTIONS
      1.   Company shall not disclose any Confidential Information to third parties following the date of its disclosure by VALVE to Company. Company shall only use the Confidential Information for pursuing Company’s business relationship with VALVE, and only as expressly permitted in this Agreement.
      2.   Company shall take reasonable security precautions, at least as great as the precautions it takes to protect its own confidential information, to keep confidential the Confidential Information.  Company may disclose Confidential Information only to Company’s employees on a need-to-know basis. 
      3.   Company may not reverse-engineer, decompile, or disassemble any software disclosed to Company hereunder.
C.   RIGHTS AND REMEDIES
      1.   Company shall notify VALVE promptly upon discovery of any unauthorized use or disclosure of Confidential Information, or any other breach of this Agreement by Company, and will cooperate with VALVE in every reasonable way to help VALVE regain possession of the Confidential Information and prevent any further unauthorized use.
      2.   Company shall return all originals, copies, reproductions, and summaries of Confidential Information at VALVE’s request or, at VALVE’s option, certify destruction of the same.
      3.   Company acknowledges that monetary damages may not be a sufficient remedy for unauthorized disclosure or use of Confidential Information and that VALVE may seek, without waiving any other rights or remedies, such injunctive or equitable relief as may be deemed proper by a court of competent jurisdiction.
D.   MISCELLANEOUS
      1.   All Confidential Information is and shall remain the property of VALVE or its suppliers. By disclosing information to Company, VALVE does not grant any express or implied right to Company to or under any VALVE patents, copyrights, trademarks, or other proprietary or intellectual property rights.
      2.   This Agreement constitutes the entire agreement between the parties with respect to the subject matter hereof and merges all prior discussions between them as to such subject matter.  It shall not be modified except by a written agreement dated subsequent to the date of this Agreement and signed by both parties. None of the provisions of this Agreement shall be deemed to have been waived by any act or acquiescence on the part of VALVE, its agents, or employees, but only by an instrument in writing signed by an authorized officer of VALVE.  No waiver of any provision of this Agreement shall constitute a waiver of any other provision(s) or of the same provision on another occasion.
      3.   If either VALVE or Company employs attorneys to enforce any rights arising out of or in relation to this Agreement, the prevailing party shall be entitled to recover reasonable attorneys’ fees. This Agreement shall be construed and controlled by the laws of the State of Washington, and Company further consents to jurisdiction by the state and federal courts sitting in the State of Washington.  Process may be served on either party by U.S. Mail, postage paid, certified or registered, return receipt requested, or by such other method as is authorized by the Washington Long Arm Statute.
      4.   Subject to the limitations set forth in this Agreement, this Agreement will inure to the benefit of and be binding upon the parties, their successors and assigns.
      5.   If any provision of this Agreement shall be held by a court of competent jurisdiction to be illegal, invalid, or unenforceable, the remaining provisions shall remain in full force and effect.
      6.   All obligations created by this Agreement shall survive change or termination of the parties’ business relationship.
 
  ____  _         __  __ __    ___  ___ ___  ____  _____ ______  _____
 /    T| T       /  ]|  T  T  /  _]|   T   Tl    j/ ___/|      T/ ___/
Y  o  || |      /  / |  l  | /  [_ | _   _ | |  T(   \_ |      (   \_ 
|     || l___  /  /  |  _  |Y    _]|  \_/  | |  | \__  Tl_j  l_j\__  T
|  _  ||     T/   \_ |  |  ||   [_ |   |   | |  | /  \ |  |  |  /  \ |
|  |  ||     |\     ||  |  ||     T|   |   | j  l \    |  |  |  \    |
l__j__jl_____j \____jl__j__jl_____jl___j___j|____j \___j  l__j   \___j
                                                                      

void main( void ) {

	vec2 pos = (( gl_FragCoord.xy / resolution.xy ) - vec2(0.5)) * vec2(resolution.x / resolution.y, 1.0);

	float c = 0.0;
	for (float i=0.0; i<64.0; ++i) {
		float zoom = 1.0 + i * 0.05 + tan(time * 0.2) * 0.3;
		vec2 trans = vec2(tan(time * 0.3) * 0.5, tan(time * 0.4) * 0.2);
		c = plasma(pos * zoom + trans, 0.0, 2.0 / (1.0 + i));
		if (c> 0.001) break;
	}
	gl_FragColor = vec4(c * pos.x, c * pos.y, c * abs(pos.x + pos.y), 0.5) * 2.0;

}
E/AndroidRuntime(819): java.lang.RuntimeException: Unable to start activity ComponentInfo{com.agora.TractFragment/com.agora.TractFragment.MainActivity}: android.view.InflateException: Binary XML file line #7: Error inflating class fragment
 E/AndroidRuntime(819):     at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2211)
 E/AndroidRuntime(819):     at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2261)
 E/AndroidRuntime(819):     at android.app.ActivityThread.access$600(ActivityThread.java:141)
 E/AndroidRuntime(819):     at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1256)
 E/AndroidRuntime(819):     at android.os.Handler.dispatchMessage(Handler.java:99)
 E/AndroidRuntime(819):     at android.os.Looper.loop(Looper.java:137)
 E/AndroidRuntime(819):     at android.app.ActivityThread.main(ActivityThread.java:5103)
 E/AndroidRuntime(819):     at java.lang.reflect.Method.invokeNative(Native Method)
 E/AndroidRuntime(819):     at java.lang.reflect.Method.invoke(Method.java:525)
 E/AndroidRuntime(819):     at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:737)
 E/AndroidRuntime(819):     at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:553)
 02-17 16:22:28.317: E/AndroidRuntime(819):     at dalvik.system.NativeStart.main(Native Method)
 E/AndroidRuntime(819): Caused by: android.view.InflateException: Binary XML file line #7: Error inflating class fragment E/AndroidRuntime(819):    at android.view.LayoutInflater.createViewFromTag(LayoutInflater.java:713)
 E/AndroidRuntime(819):     at android.view.LayoutInflater.rInflate(LayoutInflater.java:755)
 E/AndroidRuntime(819):     at android.view.LayoutInflater.inflate(LayoutInflater.java:492)
 E/AndroidRuntime(819):     at android.view.LayoutInflater.inflate(LayoutInflater.java:397)
 E/AndroidRuntime(819):     at android.view.LayoutInflater.inflate(LayoutInflater.java:353)
 E/AndroidRuntime(819):     at com.android.internal.policy.impl.PhoneWindow.setContentView(PhoneWindow.java:267)
 E/AndroidRuntime(819):     at android.app.Activity.setContentView(Activity.java:1895)
 E/AndroidRuntime(819):     at com.agora.TractFragment.MainActivity.onCreate(MainActivity.java:15)
 E/AndroidRuntime(819):     at android.app.Activity.performCreate(Activity.java:5133)
 E/AndroidRuntime(819):     at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1087)
 E/AndroidRuntime(819):     at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2175)
 E/AndroidRuntime(819):     ... 11 more
 E/AndroidRuntime(819): Caused by: android.app.Fragment$InstantiationException: Unable to instantiate fragment com.android.listfragment.ListFragment: make sure class name exists, is public, and has an empty constructor that is public
 E/AndroidRuntime(819):     at android.app.Fragment.instantiate(Fragment.java:592)
 E/AndroidRuntime(819):     at android.app.Fragment.instantiate(Fragment.java:560)
 E/AndroidRuntime(819):     at android.app.Activity.onCreateView(Activity.java:4738)
 E/AndroidRuntime(819):     at android.view.LayoutInflater.createViewFromTag(LayoutInflater.java:689)
 E/AndroidRuntime(819):     ... 21 more
 E/AndroidRuntime(819):     at dalvik.system.BaseDexClassLoader.findClass(BaseDexClassLoader.java:53)
 E/AndroidRuntime(819):     at java.lang.ClassLoader.loadClass(ClassLoader.java:501)
 E/AndroidRuntime(819):     at java.lang.ClassLoader.loadClass(ClassLoader.java:461)
 E/AndroidRuntime(819):     at android.app.Fragment.instantiate(Fragment.java:582)
 E/AndroidRuntime(819):     ... 24 more
WASHINGTON — Brain scans could be used to predict how teenagers’ mental health will fare during a stressful time, an analysis that spanned the COVID-19 pandemic suggests.

The findings, presented November 13 in a news briefing at the annual meeting of the Society for Neuroscience, may help explain why some people succumb to stress while others are more resilient.

For a lot of research, “the study happens, and you report on the results, and that’s about it,” says Margot Wagner, a bioengineer at the University of California, San Diego who was not involved in the new work. But this research followed hundreds of teenagers over time, a study design that “means you can intervene and help way sooner than otherwise,” Wagner says.

The best of Science News - direct to your inbox.  
    Headlines and summaries of the latest Science News articles, delivered to your email inbox every Thursday.

Your e-mail address
Sign Up
The pandemic was particularly tough for many teenagers, as isolation, worry and upheaval of daily routines affected them in ways that scientists are just now starting to see (SN: 1/3/23). A record number of young people are struggling with depression and anxiety, a mental health crisis that some scientists are calling “the second pandemic” (SN: 6/30/23).

While many teenagers struggled during the pandemic, others did OK. Computational neuroscientist Caterina Stamoulis of Harvard Medical School and Boston Children’s Hospital investigated why responses differed using data collected as part of the Adolescent Brain Cognitive Development, or ABCD, study. That larger study — involving scientists at 21 research sites across the United States — aims to figure out how teenagers’ brains grow over the years.

“This is the first time in history we’re looking at thousands of participants and getting these measures over time,” Wagner says. “It’s truly remarkable.”

The ABCD study, begun in 2015, was well under way when COVID hit, so researchers possessed brain scans from before the pandemic. “Without the pandemic, we would not have been able to understand the impact of a long-lasting adverse event” that deeply affected the participants’ lives, changing their interactions with their family and friends, Stamoulis says.

At the outset of the project, fMRI brain scans measured blood flow — a proxy for brain cell activity — in 1,414 teenagers, a subset of the more than 11,000 adolescents enrolled in the ABCD study. The fMRI images recorded how certain regions of the brain behave in tandem with each other, a clue that those regions work together in what neuroscientists call a brain circuit.

“Neuroimaging data is particularly useful for developing predictive models of future outcomes,” says neuroscientist and engineer Vince Calhoun of Georgia Tech, “including resilience to stress, depression and many other things.”

In May 2020, as the world shut down, researchers started surveying the teenagers in the study about how they were holding up. These surveys, sent every few months, measured aspects of mental health, stress and sadness, among other things.

Teenagers who had weaker neural connections between certain parts of the brain before the pandemic fared worse than teenagers with stronger neural connections, the team found. These brain regions included the prefrontal cortex, a brain area that gets drastically reshaped during adolescence, and the amygdala, a structure on each side of the brain that’s involved in emotions. Weaker brain connections were associated with kids having more sadness and stress during the pandemic.

Weaker and more fragile networks predicted harder times during the pandemic, Stamoulis says. But “stronger and more resilient brain networks predicted better mental health, lower stress and lower sadness.”

She and her colleagues plan to study these brain circuits as time goes on. As brains develop, they respond to experiences and environments. If those are positive, Stamoulis says, they can be “protective factors for the brain and how its circuits evolve and become wired.”

Questions or comments on this article? E-mail us at feedback@sciencenews.org | Reprints FAQ
Usage: firebase [options] [command]
Non-fatal Exception: java.lang.Exception: NullReferenceException : Object reference not set to an instance of an object.
       at I2.Loc.LanguageSourceData+<Import_Google_Coroutine>d__65.MoveNext(I2.Loc.LanguageSourceData+<Import_Google_Coroutine>d__65)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
Options:
  -V, --version                                               output the version number
  -P, --project <alias_or_project_id>                         the Firebase project to use for this command
  --account <email>                                           the Google account to use for authorization
  -j, --json                                                  output JSON instead of text, also triggers non-interactive mode
  --token <token>                                             DEPRECATED - will be removed in a future major version - supply an auth token for this command
  --non-interactive                                           error out of the command instead of waiting for prompts
  -i, --interactive                                           force prompts to be displayed
  --debug                                                     print verbose debug output and keep a debug log file
  -c, --config <path>                                         path to the firebase.json file to use for configuration
  -h, --help                                                  output usage information

  __  _   ____  _ ___ __ __ _   __ _____  __  
 /  \| | / _/ || | __|  V  | |/' _/_   _/' _/ 
| /\ | || \_| >< | _|| \_/ | |`._`. | | `._`. 
|_||_|___\__/_||_|___|_| |_|_||___/ |_| |___/ 

Commands:
  appdistribution:distribute [options] <release-binary-file>  upload a release binary
  appdistribution:testers:add [options] [emails...]           add testers to project
  appdistribution:testers:remove [options] [emails...]        remove testers from a project
  apps:create [options] [platform] [displayName]              create a new Firebase app. [platform] can be IOS, ANDROID or WEB (case insensitive).
  apps:list [platform]                                        list the registered apps of a Firebase project. Optionally filter apps by [platform]: IOS, ANDROID or WEB (case insensitive)
  apps:sdkconfig [options] [platform] [appId]                 print the Google Services config of a Firebase app. [platform] can be IOS, ANDROID or WEB (case insensitive)
  apps:android:sha:list <appId>                               list the SHA certificate hashes for a given app id. 
  apps:android:sha:create <appId> <shaHash>                   add a SHA certificate hash for a given app id.
  apps:android:sha:delete <appId> <shaId>                     delete a SHA certificate hash for a given app id.
  auth:export [options] [dataFile]                            Export accounts from your Firebase project into a data file
  auth:import [options] [dataFile]                            import users into your Firebase project from a data file(.csv or .json)
  crashlytics:symbols:upload [options] <symbolFiles...>       upload symbols for native code, to symbolicate stack traces
  crashlytics:mappingfile:generateid [options]                generate a mapping file id and write it to an Android resource file, which will be built into the app
  crashlytics:mappingfile:upload [options] <mappingFile>      upload a ProGuard/R8-compatible mapping file to deobfuscate stack traces
  database:get [options] <path>                               fetch and print JSON data at the specified path
  database:instances:create [options] <instanceName>          create a realtime database instance
  database:instances:list                                     list realtime database instances, optionally filtered by a specified location
  database:profile [options]                                  profile the Realtime Database and generate a usage report
  database:push [options] <path> [infile]                     add a new JSON object to a list of data in your Firebase
  database:remove [options] <path>                            remove data from your Firebase at the specified path
  database:set [options] <path> [infile]                      store JSON data at the specified path via STDIN, arg, or file
  database:settings:get [options] <path>                      read the realtime database setting at path
  database:settings:set [options] <path> <value>              set the realtime database setting at path.
  database:update [options] <path> [infile]                   update some of the keys for the defined path in your Firebase
  deploy [options]                                            deploy code and assets to your Firebase project
  emulators:exec [options] <script>                           start the local Firebase emulators, run a test script, then shut down the emulators
  emulators:export [options] <path>                           export data from running emulators
  emulators:start [options]                                   start the local Firebase emulators
  experimental:functions:shell [options]                      launch full Node shell with emulated functions. (Alias for `firebase functions:shell.)
  experiments:list
  experiments:describe <experiment>                           enable an experiment on this machine
  experiments:enable <experiment>                             enable an experiment on this machine
  experiments:disable <experiment>                            disable an experiment on this machine
  ext                                                         display information on how to use ext commands and extensions installed to your project
  ext:configure [options] <extensionInstanceId>               configure an existing extension instance
  ext:info [options] <extensionName>                          display information about an extension by name (extensionName@x.y.z for a specific version)
  ext:export [options]                                        export all Extension instances installed on a project to a local Firebase directory
  ext:install [options] [extensionName]                       install an official extension if [extensionName] or [extensionName@version] is provided; or run with `-i` to see all available extensions.
  ext:list                                                    list all the extensions that are installed in your Firebase project
  ext:uninstall [options] <extensionInstanceId>               uninstall an extension that is installed in your Firebase project by instance ID
  ext:update [options] <extensionInstanceId> [updateSource]   update an existing extension instance to the latest version
  firestore:delete [options] [path]                           Delete data from Cloud Firestore.
  firestore:indexes [options]                                 List indexes in your project's Cloud Firestore database.
  functions:config:clone [options]                            clone environment config from another project
  functions:config:export                                     Export environment config as environment variables in dotenv format
  functions:config:get [path]                                 fetch environment config stored at the given path
  functions:config:set [values...]                            set environment config with key=value syntax
  functions:config:unset [keys...]                            unset environment config at the specified path(s)
  functions:delete [options] [filters...]                     delete one or more Cloud Functions by name or group name.
  functions:log [options]                                     read logs from deployed functions
  functions:shell [options]                                   launch full Node shell with emulated functions
  functions:list                                              list all deployed functions in your Firebase project
  functions:secrets:access <KEY>[@version>                    Access secret value given secret and its version. Defaults to accessing the latest version.
  functions:secrets:destroy [options] <KEY>[@version>         Destroy a secret. Defaults to destroying the latest version.
  functions:secrets:get <KEY>                                 Get metadata for secret and its versions
  functions:secrets:prune [options]                           Destroys unused secrets
  functions:secrets:set [options] <KEY>                       Create or update a secret for use in Cloud Functions for Firebase.
  help [command]                                              display help information
  hosting:channel:create [options] [channelId]                create a Firebase Hosting channel
  hosting:channel:delete [options] <channelId>                delete a Firebase Hosting channel
  hosting:channel:deploy [options] [channelId]                deploy to a specific Firebase Hosting channel
  hosting:channel:list [options]                              list all Firebase Hosting channels for your project
  hosting:channel:open [options] [channelId]                  opens the URL for a Firebase Hosting channel
  hosting:clone <source> <targetChannel>                      clone a version from one site to another
  hosting:disable [options]                                   stop serving web traffic to your Firebase Hosting site
  hosting:sites:create [options] [siteId]                     create a Firebase Hosting site
  hosting:sites:delete [options] <siteId>                     delete a Firebase Hosting site
  hosting:sites:get <siteId>                                  print info about a Firebase Hosting site
  hosting:sites:list                                          list Firebase Hosting sites
  init [feature]                                              Interactively configure the current directory as a Firebase project or initialize new features in an already configured Firebase project directory.
  Non-fatal Exception: java.lang.Exception: ArgumentException : Mesh can not have more than 65000 vertices
       at UnityEngine.UI.VertexHelper.FillMesh(UnityEngine.UI.VertexHelper)
       at UnityEngine.UI.Graphic.DoMeshGeneration(UnityEngine.UI.Graphic)
       at UnityEngine.UI.Graphic.Rebuild(UnityEngine.UI.Graphic)
       at UnityEngine.UI.CanvasUpdateRegistry.PerformUpdate(UnityEngine.UI.CanvasUpdateRegistry)
       at UnityEngine.UI.CanvasUpdateRegistry:PerformUpdate(UnityEngine.UI)
  This command will create or update 'firebase.json' and '.firebaserc' configuration files in the current directory. 
  Non-fatal Exception: java.lang.Exception: NullReferenceException : Object reference not set to an instance of an object.
       at Firebase.ExceptionAggregator.ThrowAndClearPendingExceptions(Firebase.ExceptionAggregator)
       at Firebase.Platform.FirebaseHandler.Update(Firebase.Platform.FirebaseHandler)
  To initialize a specific Firebase feature, run 'firebase init [feature]'. Valid features are:
  
    - database
    - emulators
    - firestore
    - functions
    - hosting
    - hosting:github
    - remoteconfig
    - storage
  login [options]                                             log the CLI into Firebase
  login:add [options] [email]                                 authorize the CLI for an additional account
  login:ci [options]                                          generate an access token for use in non-interactive environments
  login:list                                                  list authorized CLI accounts
  login:use <email>                                           set the default account to use for this project directory
  logout [email]                                              log the CLI out of Firebase
  open [link]                                                 quickly open a browser to relevant project resources
  projects:addfirebase [projectId]                            add Firebase resources to a Google Cloud Platform project
  projects:create [options] [projectId]                       creates a new Google Cloud Platform project, then adds Firebase resources to the project
  projects:list                                               list all Firebase projects you have access to
  remoteconfig:get [options]                                  get a Firebase project's Remote Config template
  remoteconfig:rollback [options]                             roll back a project's published Remote Config template to the one specified by the provided version number
  remoteconfig:versions:list [options]                        get a list of Remote Config template versions that have been published for a Firebase project
  serve [options]                                             start a local server for your static assets
  setup:emulators:database                                    downloads the database emulator
  setup:emulators:firestore                                   downloads the firestore emulator
  setup:emulators:pubsub                                      downloads the pubsub emulator
  setup:emulators:storage                                     downloads the storage emulator
  setup:emulators:ui                                          downloads the ui emulator
  target [type]                                               display configured deploy targets for the current project
  target:apply <type> <name> <resources...>                   apply a deploy target to a resource
  target:clear <type> <target>                                clear all resources from a named resource target
  target:remove <type> <resource>                             remove a resource target
  use [options] [alias_or_project_id]                         set an active Firebase project for your working directory
xxc%                                       


Options:
  -V, --version                                               output the version number
  -P, --project <alias_or_project_id>                         the Firebase project to use for this command
  --account <email>                                           the Google account to use for authorization
  -j, --json                                                  output JSON instead of text, also triggers non-interactive mode
  --token <token>                                             DEPRECATED - will be removed in a future major version - supply an auth token for this command
  --non-interactive                                           error out of the command instead of waiting for prompts
  -i, --interactive                                           force prompts to be displayed
  --debug                                                     print verbose debug output and keep a debug log file
  -c, --config <path>                                         path to the firebase.json file to use for configuration
  -h, --help                                                  output usage information

Commands:
  appdistribution:distribute [options] <release-binary-file>  upload a release binary
  appdistribution:testers:add [options] [emails...]           add testers to project
  appdistribution:testers:remove [options] [emails...]        remove testers from a project
  apps:create [options] [platform] [displayName]              create a new Firebase app. [platform] can be IOS, ANDROID or WEB (case insensitive).
  apps:list [platform]                                        list the registered apps of a Firebase project. Optionally filter apps by [platform]: IOS, ANDROID or WEB (case insensitive)
  apps:sdkconfig [options] [platform] [appId]                 print the Google Services config of a Firebase app. [platform] can be IOS, ANDROID or WEB (case insensitive)
  apps:android:sha:list <appId>                               list the SHA certificate hashes for a given app id. 
  apps:android:sha:create <appId> <shaHash>                   add a SHA certificate hash for a given app id.
  apps:android:sha:delete <appId> <shaId>                     delete a SHA certificate hash for a given app id.
  auth:export [options] [dataFile]                            Export accounts from your Firebase project into a data file
  auth:import [options] [dataFile]                            import users into your Firebase project from a data file(.csv or .json)
  crashlytics:symbols:upload [options] <symbolFiles...>       upload symbols for native code, to symbolicate stack traces
  crashlytics:mappingfile:generateid [options]                generate a mapping file id and write it to an Android resource file, which will be built into the app
  crashlytics:mappingfile:upload [options] <mappingFile>      upload a ProGuard/R8-compatible mapping file to deobfuscate stack traces
  database:get [options] <path>                               fetch and print JSON data at the specified path
  database:instances:create [options] <instanceName>          create a realtime database instance
  database:instances:list                                     list realtime database instances, optionally filtered by a specified location
  database:profile [options]                                  profile the Realtime Database and generate a usage report
  database:push [options] <path> [infile]                     add a new JSON object to a list of data in your Firebase
  database:remove [options] <path>                            remove data from your Firebase at the specified path
  database:set [options] <path> [infile]                      store JSON data at the specified path via STDIN, arg, or file
  database:settings:get [options] <path>                      read the realtime database setting at path
  database:settings:set [options] <path> <value>              set the realtime database setting at path.
  database:update [options] <path> [infile]                   update some of the keys for the defined path in your Firebase
  deploy [options]                                            deploy code and assets to your Firebase project
  emulators:exec [options] <script>                           start the local Firebase emulators, run a test script, then shut down the emulators
  emulators:export [options] <path>                           export data from running emulators
  emulators:start [options]                                   start the local Firebase emulators
  experimental:functions:shell [options]                      launch full Node shell with emulated functions. (Alias for `firebase functions:shell.)
  experiments:list
  experiments:describe <experiment>                           enable an experiment on this machine
  experiments:enable <experiment>                             enable an experiment on this machine
  experiments:disable <experiment>                            disable an experiment on this machine
  ext                                                         display information on how to use ext commands and extensions installed to your project
  ext:configure [options] <extensionInstanceId>               configure an existing extension instance
  ext:info [options] <extensionName>                          display information about an extension by name (extensionName@x.y.z for a specific version)
  ext:export [options]                                        export all Extension instances installed on a project to a local Firebase directory
  ext:install [options] [extensionName]                       install an official extension if [extensionName] or [extensionName@version] is provided; or run with `-i` to see all available extensions.
  ext:list                                                    list all the extensions that are installed in your Firebase project
  ext:uninstall [options] <extensionInstanceId>               uninstall an extension that is installed in your Firebase project by instance ID
  ext:update [options] <extensionInstanceId> [updateSource]   update an existing extension instance to the latest version
  firestore:delete [options] [path]                           Delete data from Cloud Firestore.
  firestore:indexes [options]                                 List indexes in your project's Cloud Firestore database.
  functions:config:clone [options]                            clone environment config from another project
  functions:config:export                                     Export environment config as environment variables in dotenv format
  functions:config:get [path]                                 fetch environment config stored at the given path
  functions:config:set [values...]                            set environment config with key=value syntax
  functions:config:unset [keys...]                            unset environment config at the specified path(s)
  functions:delete [options] [filters...]                     delete one or more Cloud Functions by name or group name.
  functions:log [options]                                     read logs from deployed functions
  functions:shell [options]                                   launch full Node shell with emulated functions
  functions:list                                              list all deployed functions in your Firebase project
  functions:secrets:access <KEY>[@version>                    Access secret value given secret and its version. Defaults to accessing the latest version.
  functions:secrets:destroy [options] <KEY>[@version>         Destroy a secret. Defaults to destroying the latest version.
  functions:secrets:get <KEY>                                 Get metadata for secret and its versions
  functions:secrets:prune [options]                           Destroys unused secrets
  functions:secrets:set [options] <KEY>                       Create or update a secret for use in Cloud Functions for Firebase.
  help [command]                                              display help information
  hosting:channel:create [options] [channelId]                create a Firebase Hosting channel
  hosting:channel:delete [options] <channelId>                delete a Firebase Hosting channel
  hosting:channel:deploy [options] [channelId]                deploy to a specific Firebase Hosting channel
  hosting:channel:list [options]                              list all Firebase Hosting channels for your project
  hosting:channel:open [options] [channelId]                  opens the URL for a Firebase Hosting channel
  hosting:clone <source> <targetChannel>                      clone a version from one site to another
  hosting:disable [options]                                   stop serving web traffic to your Firebase Hosting site
  hosting:sites:create [options] [siteId]                     create a Firebase Hosting site
  hosting:sites:delete [options] <siteId>                     delete a Firebase Hosting site
  hosting:sites:get <siteId>                                  print info about a Firebase Hosting site
  hosting:sites:list                                          list Firebase Hosting sites
  init [feature]                                              Interactively configure the current directory as a Firebase project or initialize new features in an already configured Firebase project directory.
  Crashed: Thread: SIGTRAP  0x000000009207d26e
#00 pc 0x256726e libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#01 pc 0x8e1039 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#02 pc 0x1fe01e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#03 pc 0x1fe04e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#04 pc 0x54c60a libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#05 pc 0x8e1039 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#06 pc 0x10136 liblog.so (BuildId: a8e00b4bdc1cebd70ab028f91dd3fcbb)
#07 pc 0x57fb liblog.so (BuildId: a8e00b4bdc1cebd70ab028f91dd3fcbb)
#08 pc 0x25b849f libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#09 pc 0x482624a libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#10 pc 0x25c1cab libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#11 pc 0x25b6b5b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#12 pc 0x25d7a41 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#13 pc 0x25d7a0b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#14 pc 0x25671e9 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#15 pc 0x1fe02e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#16 pc 0x1fe05e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#17 pc 0x576ac4 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#18 pc 0x1fe05e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#19 pc 0x46e7a5e libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#20 pc 0x46e7a5e libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#21 pc 0x343f08b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#22 pc 0x343eff1 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#23 pc 0x32363ad libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#24 pc 0x323631d libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#25 pc 0x3236fd1 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#26 pc 0xc34dcb libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#27 pc 0xc352c9 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#28 pc 0x4874212 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#29 pc 0xbaa4e libc.so (BuildId: 59e9bbe401a851f211fc340c6c0599bb)
#30 pc 0xa7467 libc.so (BuildId: 59e9bbe401a851f211fc340c6c0599bb)
#31 pc 0x25b84ab libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#32 pc 0x4875c7a libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#33 pc 0x48985ee libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#34 pc 0x25c24af libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#35 pc 0x25c1d4b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#36 pc 0x25b84ab libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#37 pc 0x25b849f libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#38 pc 0x482624a libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#39 pc 0x25b73fd libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#40 pc 0x25d782b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#41 pc 0x6d01a9 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#42 pc 0x25c1d4b libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#43 pc 0x25d776d libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#44 pc 0x6d0199 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#45 pc 0x6d01b0 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#46 pc 0x25d76eb libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#47 pc 0x6d0199 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#48 pc 0x6d0199 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#49 pc 0x6d01b0 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#50 pc 0x6d0199 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#51 pc 0x25d75f5 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#52 pc 0x48111b2 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#53 pc 0xc3bee5 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#54 pc 0x45cd796 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#55 pc 0x45ffdae libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#56 pc 0x45ffde2 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#57 pc 0x4898406 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#58 pc 0x1fe07e libil2cpp.so (BuildId: 8f4d616f67afcc9a)
#59 pc 0x45ffdc2 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#60 pc 0x4898406 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#61 pc 0x54c60a libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#62 pc 0x486c212 libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
#63 pc 0x38228ab libmonochrome.so (BuildId: 2c8d459faa21129eb853eb9e3011cedad51fdff0)
  This command will create or update 'firebase.json' and '.firebaserc' configuration files in the current directory. 
  
  To initialize a specific Firebase feature, run 'firebase init [feature]'. Valid features are:
  
    - database
    - emulators
    - firestore
    - functions
    - hosting
    - hosting:github
    - remoteconfig
    - storage
  login [options]                                             log the CLI into Firebase
  login:add [options] [email]                                 authorize the CLI for an additional account
  login:ci [options]                                          generate an access token for use in non-interactive environments
  login:list                                                  list authorized CLI accounts
  login:use <email>                                           set the default account to use for this project directory
  logout [email]                                              log the CLI out of Firebase
  open [link]                                                 quickly open a browser to relevant project resources
  projects:addfirebase [projectId]                            add Firebase resources to a Google Cloud Platform project
  projects:create [options] [projectId]                       creates a new Google Cloud Platform project, then adds Firebase resources to the project
  projects:list                                               list all Firebase projects you have access to
  remoteconfig:get [options]                                  get a Firebase project's Remote Config template
  remoteconfig:rollback [options]                             roll back a project's published Remote Config template to the one specified by the provided version number
  remoteconfig:versions:list [options]                        get a list of Remote Config template versions that have been published for a Firebase project
  serve [options]                                             start a local server for your static assets
  setup:emulators:database                                    downloads the database emulator
  setup:emulators:firestore                                   downloads the firestore emulator
  setup:emulators:pubsub                                      downloads the pubsub emulator
  setup:emulators:storage                                     downloads the storage emulator
  setup:emulators:ui                                          downloads the ui emulator
  target [type]                                               display configured deploy targets for the current project
  target:apply <type> <name> <resources...>                   apply a deploy target to a resource
  target:clear <type> <target>                                clear all resources from a named resource target
  target:remove <type> <resource>                             remove a resource target
  use [options] [alias_or_project_id]                         set an active Firebase project for your working directory

  Components of an effective user agreement
Although it’s important to keep in mind that user agreements are customizable documents meant to be specific to the programs and services they cover, certain core elements are necessary for optimal effectiveness. Below are the most common components of a typical comprehensive user agreement. 

‌Non-exclusivity clause

A non-exclusivity clause allows third parties to license your software. This clause also clarifies that neither you nor the user has an exclusive contract with the other party.‌

Non-transferability clause
‌Non-transferability clauses protect you as the software provider from users transferring their licensure rights to other parties. This also sets the foundation for filing breach of contract lawsuits should they become necessary in the future. 

Rights after termination of contract
‌This section spells out the rights that will apply to both parties after the contract has been terminated. This type of clause safeguards you from competitors buying your software and using elements of it for their own financial gain.‌

Modifications provisions
Modifications provisions are designed to prevent users from modifying your software on the back end or altering its coding. Although some user agreements outline specific modifications that they want to prohibit, others simply issue a blanket prohibition on any modifications whatsoever. 

Breach of contract provisions
‌This section defines what constitutes a breach of contract on the user’s part. These provisions act as a guideline for users concerning how they can and can’t use your program or service. If a user violates the contract, this clause allows you to legally cancel their license without fear of legal reprisal.

Payment details 
‌A user agreement should also clarify payment details such as any fees you may charge for subscriptions or membership. 

Device specifics
‌This section describes your licensing requirements concerning user devices. For instance, some programs are designed to be used across a broad variety of devices, while others are limited to one or two. 

Liability limits
Limitations clauses protect you from litigation by users for circumstances that are beyond your control. Events covered under this clause typically include system outages and loss of data. However, these clauses won’t protect you from damage caused by gross negligence on your company’s part. 

Termination provisions
Termination clauses outline the respective rights of both parties when either one decides to terminate the user agreement. Examples include requiring the user to uninstall any software they downloaded and/or return or destroy any hard copies they may have in their possession. Most termination clauses also state that you have the right to terminate associations with customers for any or no reason. 

Choice of law clauses
‌This section defines the specific laws regarding the contractual agreement. Applicable laws are those specific to the geographic location of the provider rather than that of the end user. This clause is an essential component of any user agreement because it helps limit litigation costs in the event that contractual disputes arise.‌

Warranty disclaimers
‌Warranty disclaimers clarify that a program or service is available for use on an “as-is” basis. As the owner or operator, you aren’t responsible for making improvements or amendments to meet user needs. 

User agreement limitations
‌The major issue with most user agreements is that very few users actually read and understand them. However, this doesn’t mean that users aren’t required to abide by the terms of the agreement, provided those terms are enforceable in court. Many businesses also take a “one-and-done” approach to user agreements, but as legal contracts, these agreements are living documents that need to be updated to keep current on applicable laws and business practices. 

Types of user agreements
The two prevailing types of user agreements are called browsewraps and clickwraps. Here’s an overview of each type and how the two differ from one another. 

Browsewraps
‌Browsewrap agreements are a popular choice among some website owners because of their no-fuss appearance and function. They generally appear as a hyperlink near the bottom of the user’s screen, stating that the act of using the site implies that the user has agreed to comply with any existing user agreements. The hyperlink leads to the user agreement, but because there’s no box to check, browsewrap agreements are often found by the courts to be unenforceable. ‌

Nonetheless, some website owners believe that browsewrap agreements make potential customers less likely to click away. You can help make a browsewrap agreement more enforceable on your site by:
Non-fatal Exception: java.lang.Exception: UnityProjectNotLinkedException : To use Unity's dashboard services, you need to link your Unity project to a project ID. To do this, go to Project Settings to select your organization, select your project and then link a project ID. You also need to make sure your organization has access to the required products. Visit https://dashboard.unity3d.com to sign up.
       at Unity.Services.Core.Registration.CorePackageInitializer.Initialize(Unity.Services.Core.Registration.CorePackageInitializer)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Registration.CorePackageInitializer.Initialize(Unity.Services.Core.Registration.CorePackageInitializer)
       at Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0)
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0)
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at up.)(up)
       at Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0.<InitializeRegistryAsync>g__Fail|2(Unity.Services.Core.Internal.CoreRegistryInitializer+<>c__DisplayClass3_0)
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.CoreRegistryInitializer.InitializeRegistryAsync(Unity.Services.Core.Internal.CoreRegistryInitializer)
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult].Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[TResult])
       at Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0.<InitializeServicesAsync>g__InitializePackagesAsync|1(Unity.Services.Core.Internal.UnityServicesInternal+<>c__DisplayClass22_0)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeServicesAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.Internal.UnityServicesInternal.InitializeAsync(Unity.Services.Core.Internal.UnityServicesInternal)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncTaskMethodBuilder)
       at Unity.Services.Core.UnityServices.InitializeAsync(Unity.Services.Core.UnityServices)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at Purchaser.Init(Purchaser)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start[TStateMachine](System.Runtime.CompilerServices.AsyncVoidMethodBuilder)
       at Purchaser.Init(Purchaser)
       at GM+<Start>d__7.MoveNext(GM+<Start>d__7)
       at UnityEngine.SetupCoroutine.InvokeMoveNext(UnityEngine.SetupCoroutine)
       at System.Runtime.CompilerServices.AsyncVoidMethodBuilder:Start(System.Runtime.CompilerServices)
       at UnityEngine.SetupCoroutine:InvokeMoveNext(UnityEngine)
Placing it in a highly visible location on the page, like at the top
Using a font that makes it stand out from other page elements
Stating in plain language that users are legally bound by the user agreement‌
Clickwraps
‌Clickwrap agreements require users to consent to using the program or service within the parameters of its user agreement. This is usually done by checking a box. Some agreements have the checkbox placed at the end and require users to scroll through prior to checking it. 

Clickwrap agreements are far more legally enforceable than their browsewrap counterparts. Because it’s necessary for users to check a box, click on a button, or otherwise indicate that they’ve read the agreement, providers can limit their risks without negatively impacting the user experience. Clickwrap agreements can be used for a variety of user agreements, including login and sign-up pages.

Creating and managing user agreements
After determining that your website needs a user agreement, the next step is to get a big-picture idea of what you want your agreement to do for you. Because laws vary across states—and even municipal and county jurisdictions, in some cases—keep in mind that the more your user agreement is customized to address the specific needs of you and your customers, the more likely it will be found enforceable in a court of law. 

       _      _                    _     _       
      | |    | |                  (_)   | |      
  __ _| | ___| |__   ___ _ __ ___  _ ___| |_ ___ 
 / _` | |/ __| '_ \ / _ \ '_ ` _ \| / __| __/ __|
| (_| | | (__| | | |  __/ | | | | | \__ \ |_\__ \
 \__,_|_|\___|_| |_|\___|_| |_| |_|_|___/\__|___/
                                                 
                                                 

‌User agreements can be difficult and time-consuming to manage due to cumbersome, separate data storage systems that don’t talk to each other and a lack of transparency in the contract process. Fortunately, state-of-the-art technology has a solution in the form of digital contract management.
Fatal Exception: java.lang.Error: FATAL EXCEPTION [pool-28-thread-1]
Unity version     : 2022.3.0f1
Device model      : OnePlus OnePlus8Pro
Device fingerprint: OnePlus/IN2023/OnePlus8Pro:11/QKR1.191246.002/2006701342:user/release-keys
CPU supported ABI : [x86_64, arm64-v8a, x86, armeabi-v7a, armeabi]
Build Type        : Release
Scripting Backend : IL2CPP
Libs loaded from  : lib/arm64
Strip Engine Code : true

Digital contract management for user agreements
npm ERR! code ENOENT
npm ERR! syscall open
npm ERR! path /Users/sehwanlim/package.json
npm ERR! errno -2
npm ERR! enoent ENOENT: no such file or directory, open '/Users/sehwanlim/package.json'
npm ERR! enoent This is related to npm not being able to find a file.
npm ERR! enoent 

npm ERR! A complete log of this run can be found in:
npm ERR!     /Users/sehwanlim/.npm/_logs/2023-11-15T14_47_41_726Z-debug-0.log
sehwanlim@SEui-MacBookAir ~ % 



"
      ;
}

