1.CAN信息（CAN message）是一组由ID，DLC，DATA组成的数据信息，DATA最长为8个字节。每个字节有8bit共64bit信息。这里的DATA是由多个CAN信号是组成的。CAN信号的长度最小是1bit，最长64bit。CAN信号的数值与实际它所代表的物理量的值通过phy=A*x+B来计算。其中phy代表物理量的值，A为1LSB（Least Significant Bit）代表的物理值大小，也称Factor，x是CAN信号的值，B是物理量的偏移量。

2.CAN信号使用Intel的Little Endian方式排列在CAN信息DATA中，也可以是按照Motorola的Big Endian方式排列在CAN信息DATA中，具体使用哪种方式，可以有用户在CAN信号描述数据库中指定。

3.确定app设计到的所有窗口，设计界面。











