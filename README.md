# Trailmix Test Notes

#### Thank you for taking the time to evaluate my test project! I had a lot of fun on this one (and got a bit carried away). 
Below are descriptions of challenges I faced during development and justifications for my architectural decisions.

##### Play The Game: https://snysny.itch.io/asteroids-plus


1. Object Pooling

Object pooling is a no brainer for a game with the potential to have many reusable objects. My specific implementation uses scaling pool sizes 
with a minimum amount. All the data for these pools are kept in Pool Scriptable Objects that work with any GameObject. They are loaded 
directly from resources when the game starts. The implementaton allows for a very small initial pool, 
that will scale based on how many objects are required.

This system worked really well, it is extremely fast to add a new pool, the pools can be as small or as large as needed, and once I had written this code
I didn't really need to adjust the core of it at any point in development

potential improvements: 
	- Destroy objects if they are not used for long periods of time
	- Don't reload the pools on player death


2. System Events

In previous projects I have used both a single event manager, as I have here, and a more spread out system where each manager has it's own event
system and types. Both have benefits, the one I decided on here allows for the observer to filter out the events it cares about, the type
checking isn't very resource intensive and I personally prefer only having to subscribe to one event, and having anywhere be able to raise 
any event. 

The alternative is probably better for a larger project, but almost always results in huge Subscribe and Unsubscribe methods that I think are
equally as inefficient as typechecking the class

For such a small project a central event manager is, I think, perfectly functional and maintainable.

potential improvements:
	- allow observers to subscribe based on type, to limit invoke calls


3. Screen Wrapping

I tried to implement a solution here without looking anything up. I think calculating the bounds is actually quite an elegant solution, and there is 
potential to have the bounds extend to account for the size of an object, and use it for more direct calculations, 
or to teleport stuck objects somewhere less stuck.

I also found out that Unity's Renderer.IsVisible includes the scene view, which means that if something does break, observing it often 
fixes it. Making every asteroid behave like a quantum particle turns can make it very hard to debug anything that happens offscreen!

Overall, I think the solution is OK, with the addition of not allowing offscreen collisions, I am happy with it

potential improvements:
	- The Bounds could be used to account for stuck objects, and teleport them back into the play space


4. Asteroid Randomisation

This was something I decided was a requirement before I even started writing. It took a while to get all the points arranged properly, and limit 
the generation to shapes that make sense for the game, but it was ultimately quite simple and the generated randomness feels great.

It caused a bit of an issue when generating child asteroids, this is needlessly complicated I think and could probably be more simple by only 
running the generation when it is specifically called, but I was trying to avoid too many uses of GetComponent()


5. Managers and Controllers

My typical pattern for personal projects is to have a static Manager class and an in-game Controller class for a feature. The Manager being responsible
for what I consider the "back end" of a feature (getting a relevant position in space, loading or saving data, feature relevant stats, etc) and the controller 
being responsible for anything that happens "Live" (Instantiating objects, affecting UI or player controlled entities, etc). This pattern works really well for me
as a Controller is always a monobehaviour in the scene, that is doing scene relevant things. Anything that doesn't exist in the scene, or require the scene
to work can be made static and moved to a manager class. The exception to this is GameManager, as it has no static class in this case.

I find this pattern to help keep features responsible only for their own information and gameplay responsibilities, and avoids collosal central manager classes.

I also find it usually eliminates the need for singletons almost all of the time, and the less singletons the better generally.

I did play with the idea of creating a central Service Locator for all managers, turning all managers into interfaces and having any manager essentially be
plug and play with any implementation of itself, for instance you could write an entirely new ObjectPoolManager, implement the interface, and the rest
of the game would function as-is. I think this would be overkill for this project, as I knew I would never be extending these classes, but in the case of
a production project, I would want that in from the start.


6. Why Arent you using Unity's new input system?

I am familiar with Unity's new input system and it is very cool, event based UI input is fantastic, instant input swapping is great, etc.

In this case since I only had very few buttons and very little input to manage, I thought it would be better to avoid it. the implementations
for movement and shooting really don't require multiple mappings and there's not much point implementing events for buttons that are only
used in one place.

### This was a really fun task! I will add a few more tweaks and bugfixes as I play it more and test it.
Thanks again for taking the time to appraise my work, and I look forward to discussing it!


	  
	 